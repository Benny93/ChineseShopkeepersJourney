using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace com.vollmergames
{
    public class FinalScoreUI : MonoBehaviour
    {
        public TextMeshProUGUI scoreField;


        public void SetScoreField(int finalScore, int highscore)
        {
            if (highscore > 0)
            {
                if (finalScore > highscore)
                {
                    scoreField.text = string.Format("Score {0}\nNew high score!", finalScore);
                }
                else
                {
                    scoreField.text = string.Format("Score {0} \n High score {1}", finalScore, highscore);
                }
            }
            else
            {
                scoreField.text = string.Format("Score {0}", finalScore);
            }

        }

        private void OnEnable()
        {
            var lastScore = GameManager.Instance.GetLastScore();

            var finalScore = GameManager.Instance.GetScore();
            SetScoreField(finalScore, lastScore);
        }
    }
}