using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.News
{
    public class InformationPaper : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private List<CanvasGroup> groups;

        public void NewDayShowInformation(int day, InformationSO data)
        {
            title.SetText($"{day}일차");
            mainText.SetText(data.MainText);
        }

        private void CheckedTheInfo() => StartCoroutine(CheckInfo());

        private IEnumerator CheckInfo()
        {
            
            yield return null;
        }
    }
}