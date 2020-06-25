using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net;
using System.Linq;

namespace HD
{
    public class TcpConnectedClient
    {
        #region Data
        /// <summary>
        /// For Clients, the connection to the server.
        /// For Servers, the connection to a client.
        /// </summary>
        readonly TcpClient connection;
        private int messageCnt;

        readonly byte[] readBuffer = new byte[5000];

        NetworkStream stream
        {
            get
            {
                return connection.GetStream();
            }
        }
        #endregion

        #region Init
        public TcpConnectedClient(TcpClient tcpClient)
        {
            this.connection = tcpClient;
            this.connection.NoDelay = true; // Disable Nagle's cache algorithm
            if (TCPChat.instance.isServer)
            { // Client is awaiting EndConnect
                stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
            }
        }

        internal void Close()
        {
            connection.Close();
        }
        #endregion

        #region Async Events
        void OnRead(IAsyncResult ar)
        {
            int length = stream.EndRead(ar);
            if (length <= 0)
            { // Connection closed
                TCPChat.instance.OnDisconnect(this);
                return;
            }

            string newMessage = System.Text.Encoding.UTF8.GetString(readBuffer, 0, length);
            TCPChat.messageToDisplay += newMessage + Environment.NewLine;

            if (TCPChat.instance.isServer)
            {
                TCPChat.BroadcastChatMessage(newMessage);
            }

            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }

        internal void EndConnect(IAsyncResult ar)
        {
            connection.EndConnect(ar);

            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }
        #endregion

        #region API
        internal void SendCli(string message)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            Debug.Log(BitConverter.ToString(buffer));
            stream.Write(buffer, 0, buffer.Length);
        }

        internal void SendDataCli(Double[] PosA)
        {
            messageCnt++;
            Debug.Log("Sending");
            int len = PosA.Length;

            byte[] type = new byte[4];
            byte[] amount = new byte[4];
            byte[] buf = new byte[8];

            int full = PosA.Length * buf.Length + amount.Length * 4;
            byte[] readyData = new byte[full];

            type = System.BitConverter.GetBytes(0xf1f1f1f1);
            amount = System.BitConverter.GetBytes(len);

            type.CopyTo(readyData, 0);
            amount.CopyTo(readyData, 4);

            
            for (int i = 0; i < len; i++)
            {
                buf = System.BitConverter.GetBytes(PosA[i]);
                buf.CopyTo(readyData, (i + 1) * 8);
            }

            type = System.BitConverter.GetBytes(messageCnt);
            type.CopyTo(readyData, full-8);

            type = System.BitConverter.GetBytes(0xf2f2f2f2);
            type.CopyTo(readyData, full-4);

            stream.Write(readyData, 0, readyData.Length);
        }
        #endregion
    }
}
