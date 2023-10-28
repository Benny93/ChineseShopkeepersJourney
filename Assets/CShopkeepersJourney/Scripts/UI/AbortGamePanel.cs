using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.vollmergames
{

    public class AbortGamePanel : MonoBehaviour
    {
        public Button QuitSessionButton;

        private void Awake()
        {
            QuitSessionButton.onClick.AddListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            Debug.Log("AbortGamePanel Aborting game");
            GameManager.Instance.AbortGame();
        }
    }

}
