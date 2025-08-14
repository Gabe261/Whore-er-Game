using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    // interaction counts to record player's data on an object
    private int interactionClickCount = 0;
    private int interactionHoverCount = 0;
    
    // True if the player is actively hovering over an object.
    private bool isBeingHovered;
    
    // TODO: Make events pass a list of GameObjects so that multiple game objects
    // can be referenced for highlight effects. Also allow for interactor parent
    // reference for nested interactors.
    
    // Unity Events for the interactions on the object
    public UnityEvent<GameObject> OnMouseEnter;
    public UnityEvent<GameObject> OnMouseClick;
    public UnityEvent<GameObject> OnMouseExit;

    public UnityEvent<GameObject> OnInteractionKeyPressed;
    public bool hasCustomKeyInteraction;
    [SerializeField] private KeyCode customInteractionKey;
    public UnityEvent<GameObject> OnCustomKeyPressed;
    
    // The highlight profile, reticle shape, and tooltip profile.
    [SerializeField] private HighlightProfileSO hoverHighlightProfile;
    public bool hasHighlightProfile;
    [SerializeField] private ReticleShapeTypes hoverReticleShape;
    public bool hasReticleShape;
    [SerializeField] private TooltipProfileSO hoverTooltipProfile;
    public bool hasTooltipProfile;
    
    private void Awake()
    {
        OnMouseEnter ??= new();
        OnMouseExit ??= new();
        OnMouseClick ??= new();
        
        OnInteractionKeyPressed ??= new();
        OnCustomKeyPressed ??= new();
        
        OnMouseEnter.AddListener(SetHoverStateActive);
        OnMouseExit.AddListener(SetHoverStateInactive);
        OnMouseClick.AddListener(OnClick);
        
        OnInteractionKeyPressed.AddListener(InteractionKeyPressed);
        OnCustomKeyPressed.AddListener(CustomKeyPressed);
    }
    
    // Clean up when this object is disabled.
    private void OnDisable()
    {
        OnMouseEnter.RemoveListener(SetHoverStateActive);
        OnMouseExit.RemoveListener(SetHoverStateInactive);
        OnMouseClick.RemoveListener(OnClick);
        
        OnInteractionKeyPressed.RemoveListener(InteractionKeyPressed);
        OnCustomKeyPressed.RemoveListener(CustomKeyPressed);
    }

    // Has been hovered/clicked checks.
    public bool HasBeenHovered()
    {
        if(interactionHoverCount > 0)
            return true;
        return false;
    }
    public bool HasBeenClicked()
    {
        if(interactionClickCount > 0)
            return true;
        return false;
    }
    
    // Mouse enter, exit, and click events listeners.
    private void SetHoverStateActive(GameObject interactor)
    {
        isBeingHovered = true;
        interactionHoverCount++;

        // TODO: Initialize highlight helper, reticle, and tooltip on start/awake. 
        
        if (GetHighlightProfile() != null)
        {
            InteractorHighlightHelper highlightHelper = FindFirstObjectByType<InteractorHighlightHelper>();
            highlightHelper.StartInteractorHighlight(this.gameObject, GetHighlightProfile());
        }

        if (GetReticleShape() != ReticleShapeTypes.None)
        {
            Reticle reticle = FindFirstObjectByType<Reticle>();
            reticle.SetReticleShape(GetReticleShape());
        }

        if (GetTooltipProfile() != null)
        {
            Tooltip tooltip = FindFirstObjectByType<Tooltip>();
            tooltip.HandleDisplayContent(GetTooltipProfile());
        }
    }
    private void SetHoverStateInactive(GameObject interactor)
    {
        // TODO: Same as the SetHoverStateActive, get references on start/awake
        
        isBeingHovered = false;
        if (GetHighlightProfile() != null)
        {
            InteractorHighlightHelper highlightHelper = FindFirstObjectByType<InteractorHighlightHelper>();
            highlightHelper.StopInteractorHighlight(this.gameObject);
        }
        
        Reticle reticle = FindFirstObjectByType<Reticle>();
        reticle.SetReticleShape(ReticleShapeTypes.Dot);
        
        if (GetTooltipProfile() != null)
        {
            Tooltip tooltip = FindFirstObjectByType<Tooltip>();
            tooltip.Hide();
        }
    }
    private void OnClick(GameObject interactor)
    {
        interactionClickCount++;
    }

    // Button press event listeners.
    private void InteractionKeyPressed(GameObject interactor)
    {
        // Pressed E while hovering the object.
    }
    private void CustomKeyPressed(GameObject interactor)
    {
        if(!hasCustomKeyInteraction) { return; }
        // Pressed the selected custom interaction button while hovering on the object.
    }
    
    // Get hover profiles
    public HighlightProfileSO GetHighlightProfile()
    {
        if(!hasHighlightProfile) { return null; }
        return hoverHighlightProfile;
    }
    public ReticleShapeTypes GetReticleShape()
    {
        if(!hasReticleShape) { return ReticleShapeTypes.None; }
        return hoverReticleShape;
    }
    public TooltipProfileSO GetTooltipProfile()
    {
        if(!hasTooltipProfile) { return null; }
        return hoverTooltipProfile;
    }
}
