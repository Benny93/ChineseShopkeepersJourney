using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.vollmergames {
    public class LevelListUI : MonoBehaviour
    {
        public GameObject LevelListItemPrefab;
        public List<LevelSettings> AvailableSettings;
        public GameObject ScrollViewContainer;
        public UIPanelManager PanelManager;

        private void Start()
        {
            foreach (var setting in AvailableSettings)
            {
                GameObject go = Instantiate(LevelListItemPrefab, Vector3.zero, Quaternion.identity, ScrollViewContainer.transform);                
                go.SetActive(true);
                go.GetComponent<RectTransform>().SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

                var selectItem = go.GetComponent<LevelSelectItemUI>();

                selectItem.LevelSettings = setting;
                selectItem.Init();
                selectItem.OnButtonClick += HandleButtonClick;

            }
        }

        private void HandleButtonClick(LevelSelectItemUI item)
        {            
            
            GameManager.Instance.SetCurrentLevel(item.LevelSettings);
            PanelManager.NextPanel();
        }
    }
}