using System;
using System.Collections.Generic;
using Script.Papers;
using Script.SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.GameManager.Components
{
    public class AppearingNewsManager : MonoBehaviour
    {
        [SerializeField] private List<NewsListSO> newspaperList;
        [SerializeField] private List<NewsSO> toDayAppearingList = new();
        [SerializeField] private NewsRequest paper;
        
        private GameManager _manager;

        private void Awake()
        {
            _manager = GetComponentInParent<GameManager>();
        }

        public int NewDay(int day)
        {
            var today = newspaperList[day-1];
            toDayAppearingList.Clear();
            foreach (var newspaper in today.NewsPaperList)
            {
                toDayAppearingList.Add(newspaper);
            }
            int len = toDayAppearingList.Count;
            for (int i = 0; i < len; i++)
            {
                int ran = Random.Range(0, len);
                (toDayAppearingList[i], toDayAppearingList[ran]) = (toDayAppearingList[ran], toDayAppearingList[i]);
            }
            return today.AppearingLimit;
        }

        [ContextMenu("Request")]
        public void Request()
        {
            paper.GetNewspaper(toDayAppearingList[0]);
            toDayAppearingList.RemoveAt(0);
        }
    }
}