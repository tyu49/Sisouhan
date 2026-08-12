using System.Collections.Generic;
using UnityEngine;

namespace Script.StaticClass
{
    public static class InteractChange
    {
        public static void TurnOn(List<CanvasGroup> groups)
        {
            foreach (var group in groups)
            {
                group.interactable = true;
            }
        }
        public static void TurnOff(List<CanvasGroup> groups)
        {
            foreach (var group in groups)
            {
                group.interactable = false;
            }
        }
    }
}