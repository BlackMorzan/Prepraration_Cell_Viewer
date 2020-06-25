using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace HD
{

    public class TCPChat : MonoBehaviour
    {
        #region Data
        public static TCPChat instance;
        // Out - is always Server
        public bool isServer;

        /// <summary>
        /// IP for clients to connect to. Null if you are the server.
        /// </summary>
        // Out - is always Server
        //public IPAddress serverIp;

        /// <summary>
        /// For Clients, there is only one and it's the connection to the server.
        /// For Servers, there are many - one per connected client.
        /// </summary>
        // list for clients
        List<TcpConnectedClient> clientList = new List<TcpConnectedClient>();

        /// <summary>
        /// The string to render in Unity.
        /// </summary>
        public static string messageToDisplay;
        public static string DataToDisplay;
        public Text text;
        public Text textData;

        /// <summary>
        /// Accepts new connections.  Null for clients.
        /// </summary>
        TcpListener listener;
        #endregion

        #region Unity Events

        public void Awake()
        {
            instance = this;

            //if(serverIp == null)
            //{ // Server: start listening for connections
            //  //this.isServer = true;
            listener = new TcpListener(localaddr: IPAddress.Any, port: Globals.port);
            listener.Start();
            listener.BeginAcceptTcpClient(OnServerConnect, null);
            //}

            //else
            //{ // Client: try connecting to the server
            //  TcpClient client = new TcpClient();
            //  TcpConnectedClient connectedClient = new TcpConnectedClient(client);
            //  clientList.Add(connectedClient);
            //  client.BeginConnect(serverIp, Globals.port2, (ar) => connectedClient.EndConnect(ar), null);
            //}
        }

        protected void OnApplicationQuit()
        {
            listener?.Stop();
            for (int i = 0; i < clientList.Count; i++)
            {
                clientList[i].Close();
            }
        }

        protected void Update()
        {
            text.text = messageToDisplay;
            textData.text = DataToDisplay;
        }
        #endregion

        #region Async Events
        void OnServerConnect(IAsyncResult ar)
        {
            TcpClient tcpClient = listener.EndAcceptTcpClient(ar);
            clientList.Add(new TcpConnectedClient(tcpClient));
            TCPChat.messageToDisplay += "AsyncConnect" + Environment.NewLine;
            listener.BeginAcceptTcpClient(OnServerConnect, null);
        }
        #endregion

        #region API
        public void OnDisconnect(TcpConnectedClient client)
        {
            clientList.Remove(client);
        }

        internal void Send(string message)
        {
            BroadcastChatMessage(message);

            if (isServer)
            {
                messageToDisplay += message + Environment.NewLine;
            }
        }

        internal static void BroadcastChatMessage(string message)
        {
            for (int i = 0; i < instance.clientList.Count; i++)
            {
                TcpConnectedClient client = instance.clientList[i];
                client.SendCli(message);
            }
        }

        internal void SendData(double[] PosA)
        {
            BroadcastData(PosA);

            for (int i=0; i<PosA.Length; i++)
                DataToDisplay += PosA[i] + Environment.NewLine;
            
        }

        internal static void BroadcastData(double[] PosA)
        {
            //Debug.Log("hi Broadcast");
            for (int k = 0; k < instance.clientList.Count; k++)
            {
                //Debug.Log("hi Broadcast" + i);
                TcpConnectedClient client = instance.clientList[k];
                client.SendDataCli(PosA);
            }
            /////////////////////////////////////////////////////////////NIEPOTRZEBNE//////////////////////////////////////////////////
            ///
            /*
            byte[] buf = new Byte[8];
            int len = PosA.Length;
            byte[] readyData = new Byte[len * 8];

            for (int k=0; k<len; k++)
            {
                buf = System.BitConverter.GetBytes(PosA[k]);
                buf.CopyTo(readyData, k*8);
                
                Debug.Log("here byte " + k + " : " + BitConverter.ToString(buf) + " here: " + BitConverter.ToDouble(buf, 0) );
            }
            */
            int len = PosA.Length;
            byte[] type = new Byte[4];
            //byte type = 0xff;
            byte[] amount = new Byte[4];
            byte[] buf = new Byte[8];
            int full = PosA.Length * buf.Length + amount.Length*3;
            //byte[] message = new byte[len* + 5];

            
            type = System.BitConverter.GetBytes(0xf1f1f1f1);
            amount = System.BitConverter.GetBytes(len);

            byte[] readyData = new Byte[full];// len * 8 + 8];

            type.CopyTo(readyData, 0);
            amount.CopyTo(readyData, 4);
            
            for (int i = 0; i < len; i++)
            {
                buf = System.BitConverter.GetBytes(PosA[i]);
                buf.CopyTo(readyData, (i+1)*8);
                //Debug.Log("int " + i);
                //Debug.Log("here byte " + i + " : " + BitConverter.ToString(buf) + " here: " + BitConverter.ToDouble(buf, 0));
            }

            type = System.BitConverter.GetBytes(0xf2f2f2f2);
            type.CopyTo(readyData, full - 4);


            //for (int j=0; j<len*8+5; j++)
            if (readyData[0] == 0xf1 && readyData[1] == 0xf1 && readyData[2] == 0xf1 && readyData[3] == 0xf1)
            Debug.Log("Type1");

            //Debug.Log("Amount: " + BitConverter.ToUInt16(readyData, 0));
            //Debug.Log("Amount: " + BitConverter.ToUInt16(readyData, 4));

            for (int j = 0; j < len; j++)
            {
                ;
                //Debug.Log("here byte " + j + " : " + BitConverter.ToDouble(readyData, 8 * (j+1)));
            }
            //Debug.Log(full);

            if (readyData[full-4] == 0xf2 && readyData[full - 3] == 0xf2 && readyData[full - 2] == 0xf2 && readyData[full-1] == 0xf2)
                Debug.Log("TypeEnd");

                
            //////////////////////////////////////////////////////////////////////////////////////////
        }
        #endregion
    }
}
