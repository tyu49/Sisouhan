using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.SO
{
    [CreateAssetMenu(fileName = "Newspaper", menuName = "NewsPaper/data", order = 0)]
    public class NewspaperSO : ScriptableObject
    {
        [field:SerializeField] public string ManagingID { get; private set; }
        [field:SerializeField] public int AppearingDay { get; private set; }
        [field:SerializeField, TextArea(6,120)] public string Text { get; private set; }
        [field:SerializeField] public List<EffectEntry> EffectEntryList { get; private set; }
        [field:SerializeField, TextArea(6,120)] public string ApprovedText { get; private set; }
        
    }

    [Serializable]
    public class EffectEntry
    {
        [field: SerializeField] public EffectType Effect { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }

    public enum EffectType
    {
        Revolution,
        Reliability,
        Like
        
    }
}