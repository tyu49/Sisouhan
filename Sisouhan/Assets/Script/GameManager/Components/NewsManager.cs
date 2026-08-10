using System;
using System.Collections.Generic;
using Script.Papers;
using Script.SO;
using UnityEngine;

namespace Script.GameManager.Components
{
    public class NewsManager : MonoBehaviour
    {
        [SerializeField] private ShowingResult showingResult;
        
        private GameManager _manager;
        [field : SerializeField]public List<NewsSO> ApprovedNewspapers { get; private set; } = new();
        [field : SerializeField]public List<NewsSO> RejectedNewspapers { get; private set; } = new();
        public string ResultText { get; private set; }
        public string ResultTitle { get; private set; }

        public event Action OnValueChanged;

        private void Awake()
        {
            _manager = GetComponentInParent<GameManager>();

            _manager.OnNewDay += NewDay;
        }

        private void NewDay()
        {
            ApprovedNewspapers.Clear();
            RejectedNewspapers.Clear();
        }

        public void Approve(NewsSO news)
        {
            ApprovedNewspapers.Add(news);
        }
        public void Reject(NewsSO news)
        {
            RejectedNewspapers.Add(news);
        }

        [ContextMenu("ShowResult")]
        public void ShowResult()
        {
            ResultTitle = $"{_manager.CurrentDay}일차";
            ResultText = string.Empty;
            for (int i = 0; i < ApprovedNewspapers.Count; i++)
            {
                ResultText += $"제 {i + 1}보: " + ApprovedNewspapers[i].ApprovedText+"\n";
            }
            GetResult();
            showingResult.SetResultText(ResultTitle, ResultText);
        }

        public void GetResult()
        {
            int revolution = 0;
            int reliability = 0;
            int danger = 0;
            foreach (var news in ApprovedNewspapers)
            {
                foreach (var entry in news.EffectEntryList)
                {
                    switch (entry.Effect)
                    {
                        case EffectType.Revolution:
                            revolution += entry.Value;
                            break;
                        case EffectType.Reliability:
                            reliability += entry.Value;
                            break;
                        case EffectType.Danger:
                            danger += entry.Value;
                            break;
                    }
                }
            }
            OnValueChanged?.Invoke();
        }
    }
}