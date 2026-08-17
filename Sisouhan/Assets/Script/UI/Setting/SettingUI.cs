using System;
using System.Collections;
using DG.Tweening;
using Script.InputReader;
using Script.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float fadeTime;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private FadeController fade;

    private void Awake()
    {
    }

    private void Start()
    {
        inputReader.OnEscPressed += ChangeSettingState;
    }

    private void OnDestroy()
    {
        inputReader.OnEscPressed -= ChangeSettingState;
    }

    private WaitForSeconds FadeDelay => new WaitForSeconds(fadeTime);
    private bool _isOn = false;

    public void ChangeSettingState() => StartCoroutine(ChangeState(_isOn));

    private IEnumerator ChangeState(bool isOn)
    {
        if (isOn)
        {
            group.interactable = false;
            group.DOFade(0, fadeTime);
            yield return FadeDelay;
            group.blocksRaycasts = false;
            _isOn = false;
        }
        else
        {
            group.blocksRaycasts = true;
            group.DOFade(1, fadeTime);
            yield return FadeDelay;
            group.interactable = true;
            _isOn = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoMain()
    {
        ChangeSettingState();
        fade.ChangeScene(0);
    }
}
