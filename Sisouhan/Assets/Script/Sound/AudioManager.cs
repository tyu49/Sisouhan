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

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
        
    }
}
