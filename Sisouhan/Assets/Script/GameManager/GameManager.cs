using System;
using System.Collections.Generic;
using Script.GameManager.Components;
using Script.SO;
using UnityEngine;

namespace Script.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public int _currentDay { get; private set; }
        private int _newsLimit;
        [field: SerializeField] public int Revolution { get; private set; }
        [field: SerializeField] public int Reliability { get; private set; }
        [field: SerializeField] public int Like { get; private set; }
        private AppearingNewspapersManager _dailyNews;
        private NewspaperManager _newsManager;

        public event Action OnNewDay;
        
        private void Awake()
        {
            _dailyNews = GetComponentInChildren<AppearingNewspapersManager>();
            _newsManager = GetComponentInChildren<NewspaperManager>();
        }

        private void Start()
        {
            throw new NotImplementedException();
        }

        private void Newday()
        {
            _newsLimit = _dailyNews.NewDay(_currentDay);
            OnNewDay?.Invoke();
        }

        public void GetNews()
        {
            if (_newsLimit <= 0)
            {
                _newsManager.ShowResult();
                return;
            }

            _newsLimit--;
            _dailyNews.Request();
        }

        public void Choose(NewspaperSO data, bool choice)
        {
            if(choice)
                _newsManager.Approve(data);
            else
                _newsManager.Reject(data);
        }
        
    }
}