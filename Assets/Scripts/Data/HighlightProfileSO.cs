using UnityEngine;

[CreateAssetMenu(fileName = "HighlightProfileSO", menuName = "Scriptable Objects/HighlightProfileSO")]
public class HighlightProfileSO : BaseEffectProfileSO
{
    [Range(0.1f, 1f)]
    public float speed;
    public Color color;
    public bool isBlinking;

    [HideInInspector] public Color originalColor;
    
    private InteractorHighlightHelper highlightHelper;
    
    public override void ToggleExecuteAction(bool doExecute, Interactor interactor)
    {
        if (highlightHelper == null)
            highlightHelper = FindFirstObjectByType<InteractorHighlightHelper>();

        if (highlightHelper == null)
        {
            Debug.LogWarning("No highlightHelper found in the scene!");
            return;
        }

        if (interactor == null)
        {
            Debug.LogWarning("YOU ARE MISSING THE INTERACTOR!");
            return;
        }
        else
        {
            Debug.Log("The interactor is: " + interactor.name);
        }
        
        if (doExecute)
            highlightHelper.StartInteractorHighlight(interactor, this);
        else
            highlightHelper.StopInteractorHighlight(interactor);
    }
}
