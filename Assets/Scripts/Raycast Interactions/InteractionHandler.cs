using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    // Keeps track of the interactor object currently being interacted with.
    private Interactor activeInteractor;
    
    // Raycast variables.
    private RaycastHit hit;
    private float raycastDistance = 50f;
    private Camera cam;
    
    private void Awake()
    {
        cam = Camera.main;
        activeInteractor = null;
    }

    private void Update()
    {
        RaycastToCenter();

        // TODO: create input system
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleClick();
        }
    }

    // Shoots a raycast from the center of the screen and checks to see if it was an interactor object.
    private void RaycastToCenter()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.transform.gameObject.TryGetComponent<Interactor>(out Interactor interactor))
            {
                if (interactor != activeInteractor)
                {
                    UpdateInteractorState(interactor); // Hits an interactor object
                }
            }
            else
            {
                UpdateInteractorState(null); // Hits non-Interactor collider
            }
        }
        else
        {
            UpdateInteractorState(null); // Hits nothing
        }
    }

    // When a raycast has been sent, it will update the active interactor. It recognizes if the player has just 
    // begun to hover over, is hovering over, or is exiting the object.
    private void UpdateInteractorState(Interactor newInteractor)
    {
        if (activeInteractor == null && newInteractor != null) // Entering a new object
        {
            activeInteractor = newInteractor;
            activeInteractor.OnMouseEnter.Invoke(activeInteractor.gameObject);
            return;
        } 
        
        if (newInteractor == null && activeInteractor != null) // Exiting an object
        {
            activeInteractor.OnMouseExit.Invoke(activeInteractor.gameObject);
            activeInteractor = null;
            return;
        }
        
        if (newInteractor != null && activeInteractor != null) // Exiting an object to enter a new one
        {
            activeInteractor.OnMouseExit.Invoke(activeInteractor.gameObject);
            activeInteractor = newInteractor;
            activeInteractor.OnMouseEnter.Invoke(activeInteractor.gameObject);
        }
    }

    // TODO: build input system and handle click and key presses though there.
    private void HandleClick()
    {
        if (activeInteractor != null)
        {
            //Debug.Log("Mouse click interactor");
            activeInteractor.OnMouseClick.Invoke(activeInteractor.gameObject);
        }
        else
        {
            //Debug.Log("Mouse click null");
        }
    }
}
