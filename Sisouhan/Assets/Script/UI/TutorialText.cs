using System;
using System.Collections.Generic;
using Script.SO;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private List<TutorialSO> readyList;
        [SerializeField] private RectTransform arrowTrm;
        [SerializeField] private InputReader.InputReader inputReader;

        public static TutorialText Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            group.interactable = false;
            group.blocksRaycasts = false;
            group.alpha = 0;
            text.SetText(string.Empty);
            readyList = new List<TutorialSO>();
            readyList.Clear();
            inputReader.OnNextPressed += NextTutorial;
        }

        private void OnDestroy()
        {
            Instance = null;
            inputReader.OnNextPressed -= NextTutorial;
        }

        private void NextTutorial()
        {
            if (readyList.Count <= 0)
            {
                group.interactable = false;
                group.blocksRaycasts = false;
                group.alpha = 0;
                text.SetText(string.Empty);
                return;
            }
            group.blocksRaycasts = true;
            group.alpha = 1;
            arrowTrm.anchoredPosition = readyList[0].Pos;
            arrowTrm.rotation = readyList[0].Rotate;
            text.SetText(readyList[0].Text);
            readyList.RemoveAt(0);
        }

        public void AddTutorial(TutorialSO data)
        {
            readyList.Add(data);
        }

        public void StartTutorial() => NextTutorial();


    }
}