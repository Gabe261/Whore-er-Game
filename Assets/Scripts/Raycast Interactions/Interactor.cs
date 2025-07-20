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
    
    private void Awake()
    {
        OnMouseEnter ??= new();
        OnMouseExit ??= new();
        OnMouseClick ??= new();
        
        OnMouseEnter.AddListener(SetHoverStateActive);
        OnMouseExit.AddListener(SetHoverStateInactive);
        OnMouseClick.AddListener(OnClick);
    }
    
    // Clean up when this object is disabled.
    private void OnDisable()
    {
        OnMouseEnter.RemoveListener(SetHoverStateActive);
        OnMouseExit.RemoveListener(SetHoverStateInactive);
        OnMouseClick.RemoveListener(OnClick);
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
}
