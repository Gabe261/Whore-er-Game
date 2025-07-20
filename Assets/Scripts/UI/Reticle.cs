using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Reticle : MonoBehaviour
{
    private VisualElement root, reticle;
    
    private Dictionary<ReticleShapeTypes, string> reticleShapeDictionary = new Dictionary<ReticleShapeTypes, string>();
    
    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        reticle = root.Q<VisualElement>("Reticle");
        
        reticleShapeDictionary.Add(ReticleShapeTypes.Dot, "reticle-dot");
        reticleShapeDictionary.Add(ReticleShapeTypes.Circle, "reticle-circle");
        reticleShapeDictionary.Add(ReticleShapeTypes.Square, "reticle-square");
    }

    /// <summary>
    /// Changes the shape of the reticle UI
    /// </summary>
    /// <param name="reticleShapeType"></param>
    public void SetReticleShape(ReticleShapeTypes reticleShapeType)
    {
        RemoveAllReticleStyles();
        reticle.AddToClassList(reticleShapeDictionary[reticleShapeType]);
    }
    
    /// <summary>
    /// Helper method to clear all dynamic styles applied to the reticle visual element. 
    /// </summary>
    private void RemoveAllReticleStyles()
    {
        reticle.RemoveFromClassList(reticleShapeDictionary[ReticleShapeTypes.Dot]);
        reticle.RemoveFromClassList(reticleShapeDictionary[ReticleShapeTypes.Circle]);
        reticle.RemoveFromClassList(reticleShapeDictionary[ReticleShapeTypes.Square]);
    }
}
