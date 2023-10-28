using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

namespace com.vollmergames
{


    [System.Serializable]
    public class PanelGroup
    {
        public List<GameObject> panels;
    }

    public class UIPanelManager : MonoBehaviour, ISingleton
    {
        public List<PanelGroup> panelGroups; // List to hold groups of UI panels    

        public FinalScoreUI FinalScoreUI;
     
        private int panelIndex = 0; // Current index of the panel to be shown

        private static UIPanelManager _instance;
        public static UIPanelManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            ShowPanel(); // Show the initial panel         

            GameManager.Instance.OnGameStart += HandleGameStart;
            GameManager.Instance.OnGameEnd += HandleGameEnd;
            GameManager.Instance.OnGameAbort += HandleGameAbort;
        }

        private void HandleGameAbort()
        {
            panelIndex = 1; // Level select panel
            ShowPanel();
        }

        private void HandleGameEnd()
        {
            FinalScoreUI.gameObject.SetActive(true);
        }

        private void HandleGameStart()
        {
            HideAllPanels();
        }

        private void ShowPanel()
        {
            // Disable all panels
            HideAllPanels();

            // Enable the current panel group
            if (panelIndex < panelGroups.Count)
            {
                PanelGroup currentGroup = panelGroups[panelIndex];
                foreach (GameObject panel in currentGroup.panels)
                {
                    panel.SetActive(true);
                }
            }
        }
        public void OpenSpecificPanel(GameObject panelToOpen)
        {
            // Disable all panels
            foreach (PanelGroup group in panelGroups)
            {
                foreach (GameObject panel in group.panels)
                {
                    panel.SetActive(false);
                }
            }

            // Enable the specified panel
            panelToOpen.SetActive(true);
        }

        [ContextMenu("NextPanel")]
        public void NextPanel()
        {
            panelIndex++; // Increment the panel index

            ShowPanel(); // Show the next panel
        }


        [ContextMenu("PreviousPanel")]
        public void PreviousPanel()
        {
            panelIndex--; // Increment the panel index

            ShowPanel(); // Show the next panel
        }

        public void OnBGMSliderValueChanged(float value)
        {
            //TODO audiomixer
        }

        public void OnSFXSliderValueChanged(float value)
        {
            //TODO audiomixer 
        }

        public void HideAllPanels() {
            foreach (PanelGroup group in panelGroups)
            {
                foreach (GameObject panel in group.panels)
                {
                    panel.SetActive(false);
                }
            }
        }


        public void ResetPanelUI()
        {
            panelIndex = 0;
            NextPanel();
        }

        public void ShowPanelUI()
        {
            Debug.Log("Show panel");

            ShowPanel();
        }

     

    }


}
