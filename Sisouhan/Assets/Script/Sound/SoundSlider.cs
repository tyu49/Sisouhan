using System;
using Script.UI.Setting;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Sound
{
    public class SoundSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private SoundType type;
        [SerializeField] private SoundPanel soundPanel;

        private void Awake()
        {
            soundPanel.OnPanelEnable += SetSliderValue;
        }

        private void SetSliderValue()
        {
            switch (type)
            {
                case SoundType.Master :
                    slider.value = Mathf.Pow(10f, AudioManager.Instance.Master / 20f);
                    break;
                case SoundType.BGM:
                    slider.value = Mathf.Pow(10f, AudioManager.Instance.BGM / 20f);
                    break;
                case SoundType.SFX:
                    slider.value = Mathf.Pow(10f, AudioManager.Instance.Effect / 20f);
                    break;
            } 
        }

        public void ChangeValue()
        {
            AudioManager.Instance.ChangeValue(type, slider.value);
        }
    }

    public enum SoundType
    {
        Master,
        BGM,
        SFX
    }
}