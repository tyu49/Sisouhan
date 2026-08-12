using System;
using System.Collections.Generic;
using Script.News;
using Script.SO;
using UnityEngine;

namespace Script.GameManager.Components
{
    public class NewsManager : MonoBehaviour
    {
        [SerializeField] private ScreenSetter screen;
        
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
        public void ShowResult(bool next)
        {
            if (ApprovedNewspapers.Count <= 0)
            {
                screen.SetProcess(3);
                return;
            }
            ResultTitle = string.Empty;
            ResultText = string.Empty;
            ResultTitle = ApprovedNewspapers[0].HeadLine;
            ResultText = ApprovedNewspapers[0].ApprovedText;
            screen.SetResult(ResultTitle, ResultText);
            ApprovedNewspapers.RemoveAt(0);
        }

        public void NextResult()
        {
            ShowResult(false);
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