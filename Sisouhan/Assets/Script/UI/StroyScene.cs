using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.UI
{
    public class StroyScene : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private CanvasGroup firstFade;
        [SerializeField] private float fadeDelay;
        [SerializeField] private List<string> scriptList;
        [SerializeField] private float typeDelay;
        [SerializeField] private float changeDelay;
        [SerializeField] private AudioClip bgmData;
        [SerializeField] private InputReader.InputReader inputReader;
        [SerializeField] private int typeThis;
        private bool _canBeNext;
        private bool _skipped;
        private WaitForSeconds FadeDelay => new WaitForSeconds(fadeDelay);
        private WaitForSeconds TypeDelay => new WaitForSeconds(typeDelay);
        private WaitForSeconds ChangeDelay => new WaitForSeconds(changeDelay);

        private void Awake()
        {
            StartCoroutine(ShowText());
        }

        private IEnumerator ShowText()
        {
            yield return new WaitForSeconds(2f);
            firstFade.DOFade(0, fadeDelay);
            yield return FadeDelay;
            foreach (var script in scriptList)
            {
                foreach (var ch in script)
                {
                    if (_canBeNext)
                        break;
                    text.text += ch;
                    yield return TypeDelay;
                }
                text.SetText(script);
                yield return ChangeDelay;
                _canBeNext = false;
                text.SetText(string.Empty);
            }
            if(!_skipped)
                NextScene();
        }

        private void Start()
        {
            AudioManager.Instance.ChangeBGM(bgmData);
            inputReader.OnEscPressed += NextScene;
            inputReader.OnNextPressed += SkipText;
        }

        private void OnDestroy()
        {
            inputReader.OnEscPressed -= NextScene;
            inputReader.OnNextPressed -= SkipText;
        }

        private void SkipText()
        {
            _canBeNext = true;
        }

        private void NextScene() => StartCoroutine(NextSceneCo());

        private IEnumerator NextSceneCo()
        {
            _skipped = true;
            firstFade.DOFade(1, fadeDelay);
            yield return FadeDelay;
            switch (typeThis)
            {
                case 0:
                    SceneManager.LoadScene(2);
                    break;
                case 1:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }
}