using UnityEngine;

[CreateAssetMenu(fileName = "TooltipProfileSO", menuName = "Scriptable Objects/TooltipProfileSO")]
public class TooltipProfileSO : ScriptableObject
{
    public TooltipPositionTypes Alignment;
    public Sprite TooltipIcon;
    public string TooltipText;
}
