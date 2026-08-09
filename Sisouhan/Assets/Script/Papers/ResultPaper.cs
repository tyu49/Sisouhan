using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Script.Papers
{
    public class ResultPaper : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField]private RectTransform rectTrm;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private CanvasGroup group;

        [Header("Movement")]
        [SerializeField] private Vector2 startPos;
        [SerializeField] private Vector2 endPos;
        [SerializeField] private float appearingTime;
        [SerializeField] private Vector2 disappearPos;
        [SerializeField] private float disappearingTime;

        private WaitForSeconds AppearDelay => new WaitForSeconds(appearingTime);
        private WaitForSeconds DisappearingDelay => new WaitForSeconds(disappearingTime);
        

        public void SetResultText(string header, string body)
        {
            group.interactable = false;
            title.SetText(header);
            mainText.SetText(body);
            StartCoroutine(Appear());
        }

        private IEnumerator Appear()
        {
            rectTrm.anchoredPosition = startPos;
            rectTrm.DOAnchorPos(endPos, appearingTime).SetEase(Ease.OutQuint);
            yield return AppearDelay;
            group.interactable = true;
        }

        public void Disappear() => StartCoroutine(Disappearing());

        private IEnumerator Disappearing()
        {
            group.interactable = false;
            rectTrm.DOAnchorPos(disappearPos, disappearingTime).SetEase(Ease.InOutQuint);
            yield return DisappearingDelay;
        }
    }
}