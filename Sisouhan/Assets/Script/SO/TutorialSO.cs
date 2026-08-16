using UnityEngine;

namespace Script.SO
{
    [CreateAssetMenu(fileName = "Tutorial", menuName = "Tutorial", order = 0)]
    public class TutorialSO : ScriptableObject
    {
        [field : SerializeField] public Vector2 Pos { get; private set;}
        [field : SerializeField] public Quaternion Rotate { get; private set;}
        [field : SerializeField, TextArea(6,20)] public string Text { get; private set;}
    }
}