using UnityEngine;

[CreateAssetMenu(fileName = "ReticleProfileSO", menuName = "Scriptable Objects/ReticleProfileSO")]
public class ReticleProfileSO : ScriptableObject
{
    public ReticleShapeTypes reticleShape;
    public Color reticleColor;
}
