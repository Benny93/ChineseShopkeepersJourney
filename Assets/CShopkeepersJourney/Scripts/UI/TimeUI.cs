using UnityEngine;
using TMPro;
using System;

namespace com.vollmergames
{
    public class TimerUI : MonoBehaviour
    {
        
        public TextMeshProUGUI timerText; // Reference to the TextMeshPro Text component.
        public GameObject AbortButton;

        private void Start()
        {
            timerText.gameObject.SetActive(false);
            AbortButton.SetActive(false);
            GameManager.Instance.OnGameStart += HandleGameStart;
            GameManager.Instance.OnGameEnd += HandleGameEnd;
            GameManager.Instance.OnGameAbort += HandleGameEnd;
        }

        private void HandleGameEnd()
        {
            timerText.gameObject.SetActive(false);
            AbortButton.SetActive(false);
        }

        private void HandleGameStart()
        {
            timerText.gameObject.SetActive(true);
            AbortButton.SetActive(true);
        }

        private void Update()
        {
            if (timerText != null && GameManager.Instance.IsGameRunning)
            {
                // Get the remaining time from the GameManager.
                float remainingTime = GameManager.Instance.RemainingTime;

                // Ensure the remaining time is not negative.
                remainingTime = Mathf.Max(0f, remainingTime);

                // Format the remaining time as minutes and seconds.
                int minutes = Mathf.FloorToInt(remainingTime / 60f);
                int seconds = Mathf.FloorToInt(remainingTime % 60f);

                // Update the TextMeshPro Text component with the formatted time.
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
