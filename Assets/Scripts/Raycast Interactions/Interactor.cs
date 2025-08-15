using System.Collections.Generic;
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
    [SerializeField] private ReticleProfileSO hoverReticleProfile;
    public bool hasReticleProfile;
    [SerializeField] private TooltipProfileSO hoverTooltipProfile;
    public bool hasTooltipProfile;

    private InteractorHighlightHelper highlightHelper;
    private Reticle reticle;
    private Tooltip tooltip;
    
    private List<MeshRenderer> meshRenderers;
    
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
        
        highlightHelper = FindFirstObjectByType<InteractorHighlightHelper>();
        reticle = FindFirstObjectByType<Reticle>();
        tooltip = FindFirstObjectByType<Tooltip>();

        meshRenderers = new List<MeshRenderer>();
        if (TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            meshRenderers.Add(meshRenderer);
        }
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<MeshRenderer>(out MeshRenderer childMeshRenderer))
            {
                meshRenderers.Add(childMeshRenderer);
            }
        }
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

    public List<MeshRenderer> MeshRenderers => meshRenderers;

    public List<Material> GetMaterials()
    {
        List<Material> materials = new List<Material>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            materials.Add(meshRenderer.material);
        }
        return materials;
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
            highlightHelper.StartInteractorHighlight(this, GetHighlightProfile());
        }

        if (GetReticleShape() != null)
        {
            reticle.SetReticleShape(GetReticleShape());
        }

        if (GetTooltipProfile() != null)
        {
            tooltip.HandleDisplayContent(GetTooltipProfile());
        }
    }
    private void SetHoverStateInactive(GameObject interactor)
    {
        // TODO: Same as the SetHoverStateActive, get references on start/awake
        
        isBeingHovered = false;
        if (GetHighlightProfile() != null)
        {
            highlightHelper.StopInteractorHighlight(this);
        }

        if (GetReticleShape() != null)
        {
             reticle.SetDefaultReticleProfile();
        }
        
        if (GetTooltipProfile() != null)
        {
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
    public ReticleProfileSO GetReticleShape()
    {
        if(!hasReticleProfile) { return null; }
        return hoverReticleProfile;
    }
    public TooltipProfileSO GetTooltipProfile()
    {
        if(!hasTooltipProfile) { return null; }
        return hoverTooltipProfile;
    }
}
