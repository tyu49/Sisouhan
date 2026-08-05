using UnityEngine;

[CreateAssetMenu(fileName = "InformationSO", menuName = "Information")]
public class InformationSO : ScriptableObject
{
    [field: SerializeField] public string ManagingID { get; private set; }
    [field: SerializeField] public int AppearingDay { get; private set; }
    [field: SerializeField, TextArea(6,120)] public string MainText { get; private set; }
}
