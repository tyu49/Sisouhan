using System;
using System.Collections.Generic;
using Script.GameManager.Components;
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
        private AppearingNewsManager _dailyNews;
        private NewsManager _newsManager;

        public event Action OnNewDay;
        
        private void Awake()
        {
            _dailyNews = GetComponentInChildren<AppearingNewsManager>();
            _newsManager = GetComponentInChildren<NewsManager>();
        }

        [ContextMenu("NewDay")]
        private void NewDay()
        {
            _newsLimit = _dailyNews.NewDay(CurrentDay);
            OnNewDay?.Invoke();
        }

        [ContextMenu("GetNews")]
        public void GetNews()
        {
            if (_newsLimit <= 0)
            {
                _newsManager.ShowResult(true);
                _newsManager.GetResult();
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
        
    }
}