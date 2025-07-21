using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    // interaction counts to record player's data on an object
    private int interactionClickCount = 0;
    private int interactionHoverCount = 0;
    
    // True if the player is actively hovering over an object.
    private bool isBeingHovered;
    
    // Unity Events for the interactions on the object
    public UnityEvent<GameObject> OnMouseEnter;
    public UnityEvent<GameObject> OnMouseClick;
    public UnityEvent<GameObject> OnMouseExit;

    public UnityEvent<GameObject> OnInteractionKeyPressed;
    public bool hasCustomKeyInteraction;
    [SerializeField] private KeyCode customInteractionKey;
    public UnityEvent<GameObject> OnCustomKeyPressed;
    
    // list of reactions. All will be scriptable objects and link to another class. Highlight, Reticle change, tooltip
    
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
    }
    private void SetHoverStateInactive(GameObject interactor)
    {
        isBeingHovered = false;
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
}
