using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorHighlightHelper : MonoBehaviour
{
    private Dictionary<Interactor, List<Coroutine>> interactorDictionary;

    private void Start()
    {
        interactorDictionary = new Dictionary<Interactor, List<Coroutine>>();
    }
    
    public void StartInteractorHighlight(Interactor interactor, HighlightProfileSO highlightProfile)
    {
        List<Coroutine> objectsHighlightCoroutine = new List<Coroutine>();
        
        if (interactorDictionary.ContainsKey(interactor)) { Debug.Log("Already highlighting"); return; }
        
        List<Material> materials = interactor.GetMaterials();
        foreach (Material material in materials)
        {
            objectsHighlightCoroutine.Add(StartCoroutine(HighlightObject(material, highlightProfile)));
        }
        interactorDictionary.Add(interactor, objectsHighlightCoroutine);
    }

    private IEnumerator HighlightObject(Material material, HighlightProfileSO highlightProfile)
    {
        highlightProfile.originalColor = material.color;

        yield return new WaitForEndOfFrame();

        bool doBlinking = highlightProfile.isBlinking;
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
                if(!doBlinking && returnRate >= 2) { doLoop = false; }
            }
        }
    }
    
    public void StopInteractorHighlight(Interactor interactor)
    {
        if (interactorDictionary.TryGetValue(interactor, out List<Coroutine> highlightCoroutines))
        {
            if (highlightCoroutines.Count > 0)
            {
                foreach(Coroutine coroutine in highlightCoroutines)
                    StopCoroutine(coroutine);
            }
            else
            {
                Debug.Log("Interactor Highlight Coroutine Not Found");
            }
            
            interactorDictionary.Remove(interactor);
            foreach (Material material in interactor.GetMaterials())
            {
                // material.color = interactor.GetHighlightProfile().originalColor;
            }
        }
        else
        {
            Debug.Log("Interactor is not highlighting");
        }
    }
}
