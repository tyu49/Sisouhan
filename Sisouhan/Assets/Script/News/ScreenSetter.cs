using System.Collections.Generic;
using Script.SO;
using TMPro;
using UnityEngine;

namespace Script.News
{
    public class ScreenSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI body;
        [SerializeField] private GameManager.GameManager manager;

        [SerializeField] private CanvasGroup chooseBtn;
        [SerializeField] private CanvasGroup nextBtn;

        [SerializeField] private List<InformationSO> information;

        private int _process = 0; //0 = 정보확인, 1 = 뉴스 검사, 2 = 결과 확인 3 = 다음일차 넘어가기
        private NewsSO _myData;

        public void ShowInformation(int day)
        {
            chooseBtn.interactable = false;
            nextBtn.interactable = true;
            _process = 0;
            title.SetText($"{day}차");
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
            _process = process;
        }

        public void NextBtn()
        {
            switch (_process)
            {
                case 0:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

    }
}