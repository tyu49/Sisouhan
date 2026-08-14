using System;
using Script.SO;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class ValueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private EffectType type;
        [SerializeField] private GameManager.GameManager manager;

        private void Awake()
        {
            manager.OnValueChanged += ChangeValue;
        }

        private void OnDestroy()
        {
            manager.OnValueChanged -= ChangeValue;
        }

        private void ChangeValue()
        {
            switch (type)
            {
                case EffectType.Revolution:
                    text.SetText($"혁명도 : {manager.Revolution}/100");
                    break;
                case EffectType.Reliability:
                    text.SetText($"신뢰도 : {manager.Reliability}/100");
                    break;
                case EffectType.Danger:
                    text.SetText($"위험도 : {manager.Danger}/100");
                    break;
            }
        }
    }
}