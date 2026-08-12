using System.Collections;
using System.Collections.Generic;
using Script.SO;
using Script.StaticClass;
using TMPro;
using UnityEngine;

namespace Script.News
{
    public class NewsRequest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headLine;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private List<CanvasGroup> groups;
        [SerializeField] private GameManager.GameManager manager;

        private NewsSO _myNews;


        private void Awake()
        {
        }

        public void GetNews(NewsSO data) => StartCoroutine(GetNewsCo(data));

        private IEnumerator GetNewsCo(NewsSO data)
        {
            _myNews = data;
            headLine.SetText(_myNews.HeadLine);
            text.SetText(_myNews.Text);
            InteractChange.TurnOff(groups);
            InteractChange.TurnOn(groups);
            yield break;
        }

        private void GetResult(bool choice) => StartCoroutine(GetResultCo(choice));

        private IEnumerator GetResultCo(bool choice)
        {
            InteractChange.TurnOff(groups);
            manager.Choose(_myNews, choice);
            manager.GetNews();
            yield break;
        }
    }
}