using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode mouseClickKey;
    [SerializeField] private KeyCode interactKey;
    
    // TODO: Get a more refined list on specific keys to ignore. These are initially based on movement keys and 2 base interaction keys.
    private List<KeyCode> ingnoreKeys = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftShift};
    
    public UnityEvent<KeyCode> OnMouseDown;
    public UnityEvent<KeyCode> OnInteractKeyDown;
    public UnityEvent<KeyCode> OnAnyKeyDown;

    private void Start()
    {
        OnMouseDown ??= new ();
        OnInteractKeyDown ??= new ();
        
        ingnoreKeys.Add(mouseClickKey);
        ingnoreKeys.Add(interactKey);
    }

    private void Update()
    {
        // Check any key press
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key) && key != mouseClickKey && key != interactKey && !ingnoreKeys.Contains(key))
            {
                OnAnyKeyDown?.Invoke(key);
                break; 
            }
        }
        
        // Check left mouse click
        if (Input.GetKeyDown(mouseClickKey))
        {
            OnMouseDown?.Invoke(mouseClickKey);
        }
        
        // Check if the main 'interact' key was pressed
        if (Input.GetKeyDown(interactKey))
        {
            OnInteractKeyDown?.Invoke(interactKey);
        }
    }
}
