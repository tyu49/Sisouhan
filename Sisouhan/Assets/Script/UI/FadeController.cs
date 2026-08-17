using System;
using System.Collections;
using DG.Tweening;
using Script.News;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.UI
{
    public class FadeController : MonoBehaviour
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
            manager.Ending += EndingFade;
        }

        private void OnDestroy()
        {
            screen.processthird -= NextDay;
            manager.Ending -= EndingFade;
        }

        private void NextDay() => StartCoroutine(DayChangeCo());

        private IEnumerator DayChangeCo()
        {
            text.SetText(string.Empty);
            group.blocksRaycasts = true;
            group.alpha = 1;
            yield return FadeDelay;
            screen.ChangeToAsa();
            text.SetText($"Day {manager.CurrentDay}");
            yield return TextDelay;
            text.SetText(string.Empty);
            group.alpha = 0;
            group.blocksRaycasts = false;
            screen.SetProcess(0);
        }

        private void EndingFade(int endingType)
        {
            text.SetText(string.Empty);
            group.blocksRaycasts = true;
            
            
            StartCoroutine(ChangeSceneCo(endingType));
        }

        private IEnumerator ChangeSceneCo(int index)
        {
            group.DOFade(1, fadeTime);
            yield return FadeDelay;
            SceneManager.LoadScene(index);
        }

        public void ChangeScene(int index) => EndingFade(index);
    }
}