using System.Collections;
using TMPro;
using UnityEngine;

namespace Script.Papers
{
    public class InformationPaper : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI mainText;

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