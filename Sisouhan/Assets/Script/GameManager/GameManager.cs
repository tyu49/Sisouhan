using System;
using System.Collections.Generic;
using Script.GameManager.Components;
using Script.News;
using Script.SO;
using Script.Sound;
using Script.UI;
using UnityEngine;

namespace Script.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [field : SerializeField]public int CurrentDay { get; private set; }
        private int _newsLimit;
        [field: SerializeField] public int Revolution { get; private set; }
        [field: SerializeField] public int Reliability { get; private set; } = 50;
        [field: SerializeField] public int Danger { get; private set; }
        [SerializeField] private AudioClip bgmData;
        [SerializeField] private AudioClip hitAudio;
        [SerializeField] private ScreenSetter screen;
        private AppearingNewsManager _dailyNews;
        private NewsManager _newsManager;

        public event Action OnValueChanged;
        public event Action OnNewDay;
        public event Action<int> Ending;
        
        private void Awake()
        {
            _dailyNews = GetComponentInChildren<AppearingNewsManager>();
            _newsManager = GetComponentInChildren<NewsManager>();
        }

        private void Start()
        {
            screen.SetProcess(3);
            AudioManager.Instance.ChangeBGM(bgmData);
        }

        [ContextMenu("NewDay")]
        public void NewDay()
        {
            CurrentDay++;
            if (Danger >= 100)
            {
                AudioManager.Instance.PlayClip(hitAudio);
                Ending?.Invoke(4);
            }
            else if(CurrentDay==8&&Revolution>=80)
                Ending?.Invoke(5);
            else if(CurrentDay==8&&Revolution<80)
                Ending?.Invoke(3);
            ChangeValue(-10,0,0);
            _newsLimit = _dailyNews.NewDay(CurrentDay);
            OnNewDay?.Invoke();
        }

        [ContextMenu("GetNews")]
        public void GetNews()
        {
            if (_newsLimit <= 0)
            {
                screen.SetProcess(2);
                return;
            }

            _newsLimit--;
            _dailyNews.Request();
        }

        public void Choose(NewsSO data, bool choice)
        {
            if(choice)
                _newsManager.Approve(data);
            else
                _newsManager.Reject(data);
            GetNews();
        }

        public void ChangeValue(int rev, int rel, int danger)
        {
            Revolution += rev;
            Reliability += rel;
            Danger += danger;
            if (Danger <= 0)
                Danger = 0;
            if (Reliability <= 0)
                Reliability = 0;
            if (Revolution <= 0)
                Revolution = 0;
            if (Danger >= 100)
                Danger = 100;
            if (Reliability >= 100)
                Reliability = 100;
            if (Revolution >= 100)
                Revolution = 100;
            OnValueChanged?.Invoke();
        }
        
    }
}