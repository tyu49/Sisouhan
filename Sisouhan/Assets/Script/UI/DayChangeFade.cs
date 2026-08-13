using System;
using System.Collections;
using DG.Tweening;
using Script.News;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class DayChangeFade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameManager.GameManager manager;
        [SerializeField] private ScreenSetter screen;

        [SerializeField] private float fadeTime;
        [SerializeField] private float textDelay;
        private WaitForSeconds FadeDelay => new WaitForSeconds(fadeTime);
        private WaitForSeconds TextDelay => new WaitForSeconds(textDelay);

        private void Awake()
        {
            screen.processthird += NextDay;
        }

        private void OnDestroy()
        {
            screen.processthird -= NextDay;
        }

        private void NextDay() => StartCoroutine(DayChangeCo());

        private IEnumerator DayChangeCo()
        {
            text.SetText(string.Empty);
            group.blocksRaycasts = true;
            group.DOFade(1f, fadeTime);
            yield return FadeDelay;
            screen.ChangeToAsa();
            text.SetText($"Day {manager.CurrentDay}");
            yield return TextDelay;
            text.SetText(string.Empty);
            group.DOFade(0f, fadeTime);
            yield return FadeDelay;
            group.blocksRaycasts = false;
            screen.SetProcess(0);
        }

        public void EndingFade()
        {
            group.alpha = 1f;
            text.SetText(string.Empty);
        }
    }
}