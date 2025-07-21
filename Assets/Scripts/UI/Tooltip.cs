using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Tooltip : MonoBehaviour
{
    private VisualElement root, tooltipContainer, tooltipIcon;
    private Label tooltipText;

    private Dictionary<TooltipPositionTypes, string> tooltipPositionDictionary = new Dictionary<TooltipPositionTypes, string>();
    
    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        tooltipContainer = root.Q<VisualElement>("TooltipContainer");
        tooltipIcon = root.Q<VisualElement>("TooltipIcon");
        tooltipText = root.Q<Label>("TooltipText");

        tooltipPositionDictionary.Add(TooltipPositionTypes.MiddleCenter, "tooltip-middle-center");
        tooltipPositionDictionary.Add(TooltipPositionTypes.MiddleBottom, "tooltip-middle-bottom");
        tooltipPositionDictionary.Add(TooltipPositionTypes.RightCenter, "tooltip-right-center");
        tooltipPositionDictionary.Add(TooltipPositionTypes.RightBottom, "tooltip-right-bottom");
        tooltipPositionDictionary.Add(TooltipPositionTypes.EdgeCenter, "tooltip-edge-center");
        tooltipPositionDictionary.Add(TooltipPositionTypes.EdgeBottom, "tooltip-edge-bottom");
        
        Hide();
    }

    public void HandleDisplayContent(TooltipProfileSO tooltipProfile)
    {
        RemoveAllTooltipPositionStyles();
        tooltipContainer.AddToClassList(tooltipPositionDictionary[tooltipProfile.Alignment]);
        tooltipIcon.style.backgroundImage = new StyleBackground(tooltipProfile.TooltipIcon);
        tooltipText.text = tooltipProfile.TooltipText;
        Show();
    }
    
    private void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }
    
    /// <summary>
    /// Helper method to clear all dynamic styles applied to the tooltip visual element. 
    /// </summary>
    private void RemoveAllTooltipPositionStyles()
    {
        tooltipContainer.RemoveFromClassList(tooltipPositionDictionary[TooltipPositionTypes.MiddleCenter]);
        tooltipContainer.RemoveFromClassList(tooltipPositionDictionary[TooltipPositionTypes.MiddleBottom]);
        tooltipContainer.RemoveFromClassList(tooltipPositionDictionary[TooltipPositionTypes.RightCenter]);
        tooltipContainer.RemoveFromClassList(tooltipPositionDictionary[TooltipPositionTypes.RightBottom]);
        tooltipContainer.RemoveFromClassList(tooltipPositionDictionary[TooltipPositionTypes.EdgeCenter]);
        tooltipContainer.RemoveFromClassList(tooltipPositionDictionary[TooltipPositionTypes.EdgeBottom]);
    }
}
