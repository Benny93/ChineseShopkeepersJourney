using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BayatGames.SaveGameFree;

namespace com.vollmergames
{
    public class LevelSelectItemUI : MonoBehaviour
    {
        public delegate void LevelSelectItemUIDelegate(LevelSelectItemUI item);
        public event LevelSelectItemUIDelegate OnButtonClick;

        public Button Button;
        public TextMeshProUGUI TextMeshPro;
        public LevelSettings LevelSettings;
        public TextMeshProUGUI ScoreText;

        public void Init()
        {
            var levelName = LevelSettings.levelName;

            TextMeshPro.text = levelName;
            Button.onClick.AddListener(HandleButtonClick);
            RefreshScores();

        }

        private void RefreshScores()
        {
            var score = SaveGame.Load<int>(LevelSettings.levelName, 0);
            if (score > 0)
            {
                ScoreText.text = string.Format("Score {0}", score);
            }
            else
            {
                ScoreText.text = "New";
            }
        }

        private void HandleButtonClick()
        {
            OnButtonClick?.Invoke(this);
        }

        private void OnEnable()
        {
            RefreshScores();
        }
    }
}