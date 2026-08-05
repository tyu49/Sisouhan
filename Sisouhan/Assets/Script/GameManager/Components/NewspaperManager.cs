using System;
using System.Collections.Generic;
using Script.SO;
using UnityEngine;

namespace Script.GameManager.Components
{
    public class NewspaperManager : MonoBehaviour
    {
        private GameManager _manager;
        public List<NewspaperSO> ApprovedNewspapers { get; private set; } = new();
        public List<NewspaperSO> RejectedNewspapers { get; private set; } = new();
        public string ResultText { get; private set; }

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

        public void ShowResult()
        {
            ResultText = "-신문 결과-\n";
            for (int i = 0; i < ApprovedNewspapers.Count; i++)
            {
                ResultText += $"{i + 1}호 신문 : " + ApprovedNewspapers[i].ApprovedText+"\n";
            }
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