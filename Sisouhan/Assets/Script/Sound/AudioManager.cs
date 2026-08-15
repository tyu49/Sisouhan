using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Script.Sound
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;


        public float Master { get; private set; } = 1f;
        public float BGM { get; private set; } = 1f;
        public float Effect { get; private set; } = 1f;
        public static AudioManager Instance;

        private string masterName = "MasterVol";
        private string SFXname = "SFXVol";
        private string BGMname = "BGMVol";
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void ChangeValue(SoundType type, float value)
        {
            switch (type)
            {
                case SoundType.Master :
                    Master = Mathf.Log10(value) * 20f;
                    if (value <= 0)
                    {
                        audioMixer.SetFloat(masterName, -80f);
                        return;
                    }
                    audioMixer.SetFloat(masterName, Mathf.Log10(value) * 20f);
                    break;
                case SoundType.BGM:
                    BGM = Mathf.Log10(value) * 20f;
                    if (value <= 0)
                    {
                        audioMixer.SetFloat(BGMname, -80f);
                        return;
                    }
                    audioMixer.SetFloat(BGMname, Mathf.Log10(value) * 20f);
                    break;
                case SoundType.SFX:
                    Effect = Mathf.Log10(value) * 20f;
                    if (value <= 0)
                    {
                        audioMixer.SetFloat(SFXname, -80f);
                        return;
                    }
                    audioMixer.SetFloat(SFXname, Mathf.Log10(value) * 20f);
                    break;
            }
        }
        
    }
}
