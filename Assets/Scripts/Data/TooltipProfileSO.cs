using UnityEngine;

[CreateAssetMenu(fileName = "TooltipProfileSO", menuName = "Scriptable Objects/TooltipProfileSO")]
public class TooltipProfileSO : BaseEffectProfileSO
{
    public TooltipPositionTypes Alignment;
    public Sprite TooltipIcon;
    public string TooltipText;

    private Tooltip tooltip;
    
    public override void ToggleExecuteAction(bool doExecute, Interactor interactor = null)
    {
        if (tooltip == null)
            tooltip = FindFirstObjectByType<Tooltip>();
        
        if (tooltip == null)
        {
            Debug.LogWarning("No Tooltip found in the scene!");
            return;
        }
        
        if (doExecute)
            tooltip.HandleDisplayContent(this);
        else
            tooltip.Hide();
    }
}
