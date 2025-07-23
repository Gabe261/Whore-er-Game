using UnityEngine;

[CreateAssetMenu(fileName = "HighlightProfileSO", menuName = "Scriptable Objects/HighlightProfileSO")]
public class HighlightProfileSO : ScriptableObject
{
    [Range(0.1f, 1f)]
    public float speed;
    public Color color;
    public bool isBlinking;

    [HideInInspector] public Color originalColor;
}
