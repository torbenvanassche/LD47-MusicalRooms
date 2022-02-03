using System.Collections;
using Sirenix.OdinInspector;
using UIE;
using UnityEngine;


public class SliderController : SelectableVisualElement
{
    [SerializeField, Range(0, 100), HideLabel, OnValueChanged(nameof(SliderChange))] private int value;

    private void SliderChange()
    {
        (Element as Slider).fillPercentage = value;
    }
    
    protected override IEnumerable GetElements()
    {
        return GetVisualElementsOfType<Slider>();
    }
}
