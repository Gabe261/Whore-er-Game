using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorHighlightHelper : MonoBehaviour
{

    [SerializeField] private HighlightProfileSO highlightProfile;
    private Dictionary<Interactor, Coroutine> interactorDictionary;

    private void Start()
    {
        interactorDictionary = new Dictionary<Interactor, Coroutine>();
    }

    public void StopInteractorHighlight(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Interactor interactor))
        {
            if (interactorDictionary.TryGetValue(interactor, out Coroutine highlightCoroutine))
            {
                if (highlightCoroutine != null)
                {
                    StopCoroutine(highlightCoroutine);
                }
                else
                {
                    Debug.Log("Interactor Highlight Coroutine Not Found");
                }
                
                interactorDictionary.Remove(interactor);
                Material material = interactor.gameObject.GetComponent<MeshRenderer>().material;
                material.color = new Color(1,1,1,1);
            }
            else
            {
                Debug.Log("Interactor is not highlighting");
            }
        }
    }

    public void StartInteractorHighlight(GameObject gameObject)
    {
        StartInteractorHighlight(gameObject, highlightProfile);
    }
    
    public void StartInteractorHighlight(GameObject gameObject, HighlightProfileSO highlightProfile)
    {
        Coroutine objectsHighlightCoroutine = null;

        if (gameObject.TryGetComponent(out Interactor interactor))
        {
            if (interactorDictionary.ContainsKey(interactor))
            {
                Debug.Log("Already highlighting");
                return;
            }
            
            if (highlightProfile.isBlinking)
            {
                objectsHighlightCoroutine = StartCoroutine(BlinkingHighlight(interactor, highlightProfile));
            }
            else
            {
                objectsHighlightCoroutine = StartCoroutine(SingleHighlight(interactor, highlightProfile));
            }
            
            interactorDictionary.Add(interactor, objectsHighlightCoroutine);
        }
    }

    private IEnumerator BlinkingHighlight(Interactor interactor, HighlightProfileSO highlightProfile)
    {
        Material material = interactor.gameObject.GetComponent<MeshRenderer>().material;

        bool flip = false;
        float blueValue = 1;
        while (true)
        {
            material.color = new Color(1, 1, blueValue);
            yield return new WaitForSeconds(0.01f);
            
            if (flip)
            {
                blueValue += 0.05f;
                if (blueValue > 1) { flip = false; }
            }
            else
            {
                blueValue -= 0.05f;
                if (blueValue < 0){ flip = true; }
            }
        }
    }

    private IEnumerator SingleHighlight(Interactor interactor, HighlightProfileSO highlightProfile)
    {
        yield return new WaitForSeconds(0.5f);
    }
}
