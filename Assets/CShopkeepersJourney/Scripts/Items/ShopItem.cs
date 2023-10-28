using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public ChineseLearningItem LearningItem;
    public AudioSource audioSource;
    public TextMeshProUGUI ItemName;
    public InfoCard InfoCard;

    private bool inspecting;

    private void Awake()
    {
        ItemName.text = LearningItem.englishName;
    }

    public void OnInspect()
    {
        if (inspecting) {
            return;
        }        

        inspecting = true;

        InfoCard.InitCard(LearningItem);

        var audioClip = LearningItem.audioFile;
        if (audioClip) {

            audioSource.PlayOneShot(audioClip);
        }

        
        Invoke("HideInfoCard", 2f);
        Invoke("EndInspecting", 2.2f);
    }

    public void SetLearningItem(ChineseLearningItem learningItem) {
        LearningItem = learningItem;
        ItemName.text = LearningItem.englishName;
    }
    

    private void HideInfoCard() {

        InfoCard.Hide();
    }

    private void EndInspecting() {
        inspecting = false;
    }



}
