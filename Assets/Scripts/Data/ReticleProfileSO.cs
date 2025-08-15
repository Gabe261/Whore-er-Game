using UnityEngine;

[CreateAssetMenu(fileName = "ReticleProfileSO", menuName = "Scriptable Objects/ReticleProfileSO")]
public class ReticleProfileSO : BaseEffectProfileSO
{
    public ReticleShapeTypes reticleShape;
    public Color reticleColor;
    
    private Reticle reticle;
    
    public override void ToggleExecuteAction(bool doExecute, Interactor interactor = null)
    {
        if (reticle == null)
            reticle = FindFirstObjectByType<Reticle>();
        
        if (reticle == null)
        {
            Debug.LogWarning("No Reticle found in the scene!");
            return;
        }
        
        if (doExecute)
            reticle.SetReticleShape(this);
        else
            reticle.SetDefaultReticleProfile();
    }
}
