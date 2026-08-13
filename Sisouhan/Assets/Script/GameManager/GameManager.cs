using System;
using System.Collections.Generic;
using Script.GameManager.Components;
using Script.News;
using Script.SO;
using UnityEngine;

namespace Script.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [field : SerializeField]public int CurrentDay { get; private set; }
        private int _newsLimit;
        [field: SerializeField] public int Revolution { get; private set; }
        [field: SerializeField] public int Reliability { get; private set; }
        [field: SerializeField] public int Danger { get; private set; }
        [SerializeField] private ScreenSetter screen;
        private AppearingNewsManager _dailyNews;
        private NewsManager _newsManager;

        public event Action OnValueChanged;
        
        public event Action OnNewDay;
        
        private void Awake()
        {
            _dailyNews = GetComponentInChildren<AppearingNewsManager>();
            _newsManager = GetComponentInChildren<NewsManager>();
        }

        [ContextMenu("NewDay")]
        public void NewDay()
        {
            ChangeValue(0,0,-10);
            CurrentDay++;
            _newsLimit = _dailyNews.NewDay(CurrentDay);
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
            OnValueChanged?.Invoke();
        }
        
    }
}