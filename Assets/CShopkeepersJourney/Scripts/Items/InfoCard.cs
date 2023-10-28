using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InfoCard : MonoBehaviour
{
    public TextMeshProUGUI TextPinYin;
    public TextMeshProUGUI TextHanZi;
    public GameObject parent;

    public void InitCard(ChineseLearningItem learningItem) {
        TextPinYin.text = learningItem.chineseTranslationInPinyin;
        TextHanZi.text = learningItem.writtenChinese;
        parent.SetActive(true);
    }

    internal void Hide()
    {
        parent.SetActive(false);
    }
}
