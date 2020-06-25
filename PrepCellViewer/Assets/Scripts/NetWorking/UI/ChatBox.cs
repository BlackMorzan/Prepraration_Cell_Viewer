using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace HD
{

    public class ChatBox : MonoBehaviour
    {


        public Text text;
        public int len = 9;
        //public GameObject Efector, Ball;
        public Rigidbody rb;
        private Vector3 velocity, lastPosition = new Vector3(0,0,0);
        private Vector3 acceleration, lastVelocity = new Vector3(0,0,0);
        public void Start()
        {
            lastPosition = rb.position;
        }

        public void LateUpdate()
        {
            //Debug.Log("IsSending " + Globals.isSending);
            if (Globals.isSending == true)
                SendData();
        }
        public void SendChat()
        {
            TCPChat.instance.Send(text.text);
            Debug.Log("Send " + text.text);
            text.text = "";
        }

        public void SendData()
        {
            ///
            velocity = (rb.position - lastPosition) / Time.fixedDeltaTime;
            lastPosition = rb.position;

            //acceleration = (velocity - lastVelocity) / Time.fixedDeltaTime;
            //lastVelocity = velocity;


            ////
            double[] vs = new double[len];
            vs[0] = rb.transform.position.x;
            vs[1] = rb.transform.position.z;
            vs[2] = rb.transform.position.y;
            /////
            vs[3] = velocity.x;
            vs[4] = velocity.y;
            vs[5] = velocity.z;
            vs[6] = rb.transform.rotation.eulerAngles.x;
            vs[7] = rb.transform.rotation.eulerAngles.y;
            vs[8] = rb.transform.rotation.eulerAngles.z;

            for (int i = 0; i <= 8; i++)
                Debug.Log("tran/velo/accel " + i + " : " + vs[i]);

            TCPChat.instance.SendData(vs);
        }

    }
}
