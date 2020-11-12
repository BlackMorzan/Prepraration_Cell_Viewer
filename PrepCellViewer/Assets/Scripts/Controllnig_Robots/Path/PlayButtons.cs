using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayButton
{
    public class PlayButtons : MonoBehaviour
    {
        public GameObject StopButton;
        public GameObject PlayButton;
        public GameObject PauseButton;

        private void Awake()
        {
            PlayButton.SetActive(false);
            PauseButton.SetActive(false);
        }

        public void StartPlay()
        {
            PlayButton.SetActive(true);
            StopButton.SetActive(false);
            PauseButton.SetActive(false);
        }

        public void StopPlay()
        {
            PlayButton.SetActive(false);
            StopButton.SetActive(true);
            PauseButton.SetActive(false);
        }

        public void PausePlay()
        {
            PlayButton.SetActive(false);
            StopButton.SetActive(false);
            PauseButton.SetActive(true);
        }
    }
}
