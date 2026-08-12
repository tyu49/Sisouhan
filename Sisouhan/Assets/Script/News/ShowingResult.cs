using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script.StaticClass;
using TMPro;
using UnityEngine;

namespace Script.Papers
{
    public class ShowingResult : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField]private RectTransform rectTrm;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private List<CanvasGroup> groups;

        public void SetResultText(string header, string body)
        {
            InteractChange.TurnOff(groups);
            title.SetText(header);
            mainText.SetText(body);
        }



    }
}