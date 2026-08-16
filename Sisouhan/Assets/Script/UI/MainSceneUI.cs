using System;
using System.Collections;
using DG.Tweening;
using Script.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.UI
{
    public class MainSceneUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private float fadeDelay;
        [SerializeField] private AudioClip mainBgm;

        private WaitForSeconds FadeDelay => new WaitForSeconds(fadeDelay);

        public void StartGame() => StartCoroutine(StartCo());


        private IEnumerator StartCo()
        {
            group.blocksRaycasts = true;
            group.DOFade(1, fadeDelay).SetEase(Ease.InQuad);
            yield return FadeDelay;
            SceneManager.LoadScene(1);
        }
        
        
        public void QuitGame()
        {
            Application.Quit();
        }

        private void Start()
        {
            AudioManager.Instance.ChangeBGM(mainBgm);
        }
    }
}