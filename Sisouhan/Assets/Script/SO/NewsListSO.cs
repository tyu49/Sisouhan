using System.Collections.Generic;
using UnityEngine;

namespace Script.SO
{
    [CreateAssetMenu(fileName = "NewspaperList", menuName = "Newspaper/list", order = 0)]
    public class NewsListSO : ScriptableObject
    {
        [field: SerializeField] public int AppearingDay { get; private set; }
        [field:SerializeField] public List<NewsSO> NewsPaperList { get; private set; }
        [field: SerializeField] public int AppearingLimit { get; private set; }
    }
}