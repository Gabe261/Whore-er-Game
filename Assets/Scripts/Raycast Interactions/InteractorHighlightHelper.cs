using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorHighlightHelper : MonoBehaviour
{
    private Dictionary<Interactor, Coroutine> interactorDictionary;

    private void Start()
    {
        interactorDictionary = new Dictionary<Interactor, Coroutine>();
    }
    
    public void StartInteractorHighlight(GameObject gameObject, HighlightProfileSO highlightProfile)
    {
        Coroutine objectsHighlightCoroutine = null;

        if (gameObject.TryGetComponent(out Interactor interactor))
        {
            if (interactorDictionary.ContainsKey(interactor)) { Debug.Log("Already highlighting"); return; }
            objectsHighlightCoroutine = StartCoroutine(HighlightObject(interactor, highlightProfile));
            interactorDictionary.Add(interactor, objectsHighlightCoroutine);
        }
    }

    private IEnumerator HighlightObject(Interactor interactor, HighlightProfileSO highlightProfile)
    {
        Material material = interactor.gameObject.GetComponent<MeshRenderer>().material;
        highlightProfile.originalColor = material.color;

        yield return new WaitForEndOfFrame();

        float duration = highlightProfile.speed;
        Color originalColor = material.color;
        Color targetColor = highlightProfile.color;
        float elapsedTime = 0f;

        int returnRate = 0;
        
        bool doLoop = true;
        while (doLoop)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            material.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
            if (elapsedTime >= duration)
            {
                elapsedTime = 0f;
                Color tempColor = originalColor;
                originalColor = targetColor;
                targetColor = tempColor;
                returnRate++;
                if(!highlightProfile.isBlinking && returnRate >= 2) { doLoop = false; }
            }
        }
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
                material.color = interactor.GetHighlightProfile().originalColor;
            }
            else
            {
                Debug.Log("Interactor is not highlighting");
            }
        }
    }
}
