using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.vollmergames
{


    public class SettingsPanelUI : MonoBehaviour
    {
        public Slider BGMSlider;
        public Slider SFXSlider;


        private void Start()
        {
   
            BGMSlider.onValueChanged.AddListener(OnBGMSliderValueChanged);
            SFXSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
        }

        public void OnBGMSliderValueChanged(float value)
        {
            GameManager.Instance.AudioMixer.SetFloat("MusicVolume", value);
        }

        public void OnSFXSliderValueChanged(float value)
        {
            GameManager.Instance.AudioMixer.SetFloat("SFXVolume", value);
        }


    }


}