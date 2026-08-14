using System;
using System.Collections;
using System.Collections.Generic;
using Script.GameManager.Components;
using Script.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.News
{
    public class ScreenSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI body;
        [SerializeField] private GameManager.GameManager manager;
        [SerializeField] private NewsManager newsManager;
        [SerializeField] private Image background;

        [SerializeField] private CanvasGroup chooseBtn;
        [SerializeField] private CanvasGroup nextBtn;

        [SerializeField] private List<InformationSO> information;
        [SerializeField] private List<Sprite> backgroundSprites;
        private WaitForSeconds LoadDelay => new WaitForSeconds(0.5f);

        private int _process = 0; //0 = 정보확인, 1 = 뉴스 검사, 2 = 결과 확인 3 = 다음일차 넘어가기
        private NewsSO _myData;

        public event Action processthird;
        
        public void ShowInformation(int day)
        {
            chooseBtn.interactable = false;
            nextBtn.interactable = true;
            _process = 0;
            title.SetText($"{day}일차");
            body.SetText(information[day-1].MainText);
        }
        public void SetNews(NewsSO data)
        {
            _myData = data;
            nextBtn.interactable = false;
            chooseBtn.interactable = true;
            title.SetText(data.HeadLine);
            body.SetText(data.Text);
        }
        public void Choose(bool choice) => manager.Choose(_myData, choice);

        public void SetResult(string head, string main)
        {
            chooseBtn.interactable = false;
            nextBtn.interactable = true;
            title.SetText(head);
            body.SetText(main);
        }

        public void SetProcess(int process)
        {
            title.SetText(string.Empty);
            body.SetText(string.Empty);
            chooseBtn.interactable = false;
            nextBtn.interactable = false;
            _process = process;
            switch (process)
            {
                case 0:
                    background.sprite = backgroundSprites[0];
                    StartCoroutine(ChangeScreen("정보 수집중", 0));
                    break;
                case 1:
                    StartCoroutine(ChangeScreen("뉴스 수집중", 1));
                    break;
                case 2:
                    StartCoroutine(ChangeScreen("결산중", 2));
                    break;
                case 3:
                    manager.NewDay();
                    processthird?.Invoke();
                    break;
            }
        }

        public void NextBtn()
        {
            switch (_process)
            {
                case 0:
                    SetProcess(1);
                    break;
                case 2:
                    newsManager.NextResult();
                    break;
                default:
                    break;
            }
        }

        private IEnumerator ChangeScreen(string text, int process)
        {
            for (int i = 0; i < 2; i++)
            {
                string txt = text;
                txt += ".";
                for (int j = 0; j < 3; j++)
                {
                    title.SetText(txt);
                    yield return LoadDelay;
                    txt += ".";
                }
            }

            switch (process)
            {
                case 0:
                    ShowInformation(manager.CurrentDay);
                    break;
                case 1:
                    background.sprite = backgroundSprites[1];
                    manager.GetNews();
                    break;
                case 2:
                    background.sprite = backgroundSprites[2];
                    newsManager.ShowResult();
                    newsManager.GetResult();
                    break;
            }
        }

        public void ChangeToAsa()
        {
            background.sprite = backgroundSprites[0];
        }
        
    }
}