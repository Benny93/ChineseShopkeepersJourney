using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace com.vollmergames
{
    public class ScoreUI : MonoBehaviour
    {
        public TextMeshProUGUI scoreText; // Reference to the TextMeshPro Text component.

        private void Start()
        {
            scoreText.gameObject.SetActive( false);
            GameManager.Instance.OnGameStart += HandleGameStart;
            GameManager.Instance.OnGameEnd += HandleGameEnd;
            GameManager.Instance.OnGameAbort += HandleGameEnd;
        }

        private void HandleGameEnd()
        {
            scoreText.gameObject.SetActive(false);
        }

        private void HandleGameStart()
        {
            scoreText.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (scoreText != null )
            {
                
                int score = GameManager.Instance.GetScore();                                
                scoreText.text = string.Format("Score {0}", score);
            }
        }
    }
}