using System;
using System.Collections;
using DG.Tweening;
using Script.SO;
using TMPro;
using UnityEngine;
namespace Script.NewsPaper
{
    public class NewspaperRequest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headLine;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private GameManager.GameManager manager;
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private Vector2 endPosition;
        [SerializeField] private float appearingDelay;
        [SerializeField] private Vector2 approvePosition;
        [SerializeField] private Vector2 rejectedPosition;
        [SerializeField] private float moveDelay;

        private WaitForSeconds AppearDelay => new WaitForSeconds(appearingDelay);
        private WaitForSeconds MoveDelay => new WaitForSeconds(moveDelay);
        private RectTransform _rectTrm;
        private NewspaperSO _myNews;


        private void Awake()
        {
            _rectTrm = GetComponent<RectTransform>();
        }

        public void GetNewspaper(NewspaperSO data) => StartCoroutine(GetNewspaperCo(data));

        private IEnumerator GetNewspaperCo(NewspaperSO data)
        {
            _myNews = data;
            group.interactable = false;
            _rectTrm.anchoredPosition = startPosition;
            _rectTrm.DOAnchorPos(endPosition, appearingDelay).SetEase(Ease.OutBack);
            yield return AppearDelay;
            group.interactable = true;
        }

        private void GetResult(bool choice) => StartCoroutine(GetResultCo(choice));

        private IEnumerator GetResultCo(bool choice)
        {
            group.interactable = false;
            manager.Choose(_myNews, choice);
            _rectTrm.DOAnchorPos(choice ? approvePosition : rejectedPosition, moveDelay).SetEase(Ease.OutQuint);
            yield return MoveDelay;
            manager.GetNews();
        }
    }
}