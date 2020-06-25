using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace HD
{

    public class ButtonServer : MonoBehaviour
    {
        public GameObject tcpServer;//, udp;
        public GameObject Dial;

        static GameObject step1, step;

        public void Start()
        {
            //step1 = GameObject.Find("InputField");
            step = GameObject.Find("SendButton");
            tcpServer = GameObject.Find("Server");
            Dial = GameObject.Find("ConnectDial");
            //step1.SetActive(false);
            Dial.SetActive(false);
            step.SetActive(false);
        }

        public void OnClickMine()
        {
            Globals.isServer = true;
            //step1.SetActive(true);
            step.SetActive(true);
            if (Globals.isSending == true)
            {
                Globals.isSending = false;
                Dial.SetActive(false);
            }
            else
            {
                Globals.isSending = true;
                Dial.SetActive(true);
            }

        }
    }
}
