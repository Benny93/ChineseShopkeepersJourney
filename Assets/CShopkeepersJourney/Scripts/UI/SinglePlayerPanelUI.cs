using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace com.vollmergames
{

    public class SinglePlayerPanelUI : MonoBehaviour
    {
        public TextMeshProUGUI SelectedLevelText;


        private void OnEnable()
        {            
            var lastScore = GameManager.Instance.GetLastScore();
            if (lastScore > 0)
            {
                SelectedLevelText.text = string.Format("{0} - Best score: {1}", GameManager.Instance.currentLevel.name, lastScore);
            }
            else {
                SelectedLevelText.text = GameManager.Instance.currentLevel.name;
            }            
        }
    }

}
