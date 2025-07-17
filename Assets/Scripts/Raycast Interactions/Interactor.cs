using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    // interaction counts to record player's data on an object
    private int interactionClickCount = 0;
    private int interactionHoverCount = 0;
    
    // True if the player is actively hovering over an object.
    public bool IsBeingHovered = false;
    
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
    
    // Mouse enter, exit, and click events listeners.
    private void SetHoverStateActive(GameObject interactor)
    {
        IsBeingHovered = true;
        interactionHoverCount++;
    }
    private void SetHoverStateInactive(GameObject interactor)
    {
        IsBeingHovered = false;
    }
    private void OnClick(GameObject interactor)
    {
        Debug.Log("I got clicked! \"" + interactor.name + "\"");
        interactionClickCount++;
    }
}
