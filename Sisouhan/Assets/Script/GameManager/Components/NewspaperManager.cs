using System;
using System.Collections.Generic;
using Script.NewsPaper;
using Script.SO;
using UnityEngine;

namespace Script.GameManager.Components
{
    public class NewspaperManager : MonoBehaviour
    {
        [SerializeField] private ResultPaper resultPaper;
        
        private GameManager _manager;
        [field : SerializeField]public List<NewspaperSO> ApprovedNewspapers { get; private set; } = new();
        [field : SerializeField]public List<NewspaperSO> RejectedNewspapers { get; private set; } = new();
        public string ResultText { get; private set; }
        public string ResultTitle { get; private set; }

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

        public void Approve(NewspaperSO newspaper)
        {
            ApprovedNewspapers.Add(newspaper);
        }
        public void Reject(NewspaperSO newspaper)
        {
            RejectedNewspapers.Add(newspaper);
        }

        [ContextMenu("ShowResult")]
        public void ShowResult()
        {
            ResultTitle = $"{_manager.CurrentDay}일차";
            ResultText = string.Empty;
            for (int i = 0; i < ApprovedNewspapers.Count; i++)
            {
                ResultText += $"{i + 1}호 뉴스 : " + ApprovedNewspapers[i].ApprovedText+"\n";
            }
            GetResult();
            resultPaper.SetResultText(ResultTitle, ResultText);
        }

        public void GetResult()
        {
            int revolution = 0;
            int reliability = 0;
            int like = 0;
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
                        case EffectType.Like:
                            like += entry.Value;
                            break;
                    }
                }
            }
        }
    }
}