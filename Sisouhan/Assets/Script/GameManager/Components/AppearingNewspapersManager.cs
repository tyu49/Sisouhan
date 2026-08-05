using System.Collections.Generic;
using Script.SO;
using UnityEngine;

namespace Script.GameManager.Components
{
    public class AppearingNewspapersManager : MonoBehaviour
    {
        [SerializeField] private List<NewspaperListSO> newspaperList;
        [SerializeField] private List<NewspaperSO> toDayAppearingList = new();

        public int NewDay(int day)
        {
            NewspaperListSO today = new();
            today = newspaperList[day-1];
            toDayAppearingList.Clear();
            toDayAppearingList = today.NewsPaperList;
            int len = toDayAppearingList.Count;
            for (int i = 0; i < len; i++)
            {
                int ran = Random.Range(0, len);
                (toDayAppearingList[i], toDayAppearingList[ran]) = (toDayAppearingList[ran], toDayAppearingList[i]);
            }
            return today.AppearingLimit;
        }
    }
}