using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
namespace Script.UI.Setting

{
    public class SoundPanel : MonoBehaviour
    {
        [SerializeField] private InputReader.InputReader inputReader;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private float fadeDelay;

        public event Action OnPanelEnable;
        
        private WaitForSeconds FadeDelay => new WaitForSeconds(fadeDelay);
        private bool _isOn = false;
        private void Start()
        {
            inputReader.OnEscPressed += DisableThis;
        }

        private void OnDestroy()
        {
            inputReader.OnEscPressed -= DisableThis;
        }

        private void DisableThis() => StartCoroutine(DisableCo());

        public void ChangeState()
        {
            StartCoroutine(_isOn ? DisableCo() : EnableCo());
        }
        
        private IEnumerator DisableCo()
        {
            group.interactable = false;
            group.DOFade(0, fadeDelay);
            yield return FadeDelay;
            group.blocksRaycasts = false;
            _isOn = false;
        }
        private IEnumerator EnableCo()
        {
            OnPanelEnable?.Invoke();
            group.blocksRaycasts = true;
            group.DOFade(1, fadeDelay);
            yield return FadeDelay;
            group.interactable = true;
            _isOn = true;
        }


    }
}