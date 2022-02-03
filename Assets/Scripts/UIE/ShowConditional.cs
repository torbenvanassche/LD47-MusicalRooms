using System;
using System.Collections;
using UIE;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ShowConditional : SelectableVisualElement
{
    [SerializeField] private UnityEvent condition;
    private bool show = true;
    
    protected override IEnumerable GetElements()
    {
        return GetVisualElementsOfType<VisualElement>();
    }

    public void IsNotWebGL()
    {
        show = Application.platform != RuntimePlatform.WebGLPlayer;
    }

    void Awake()
    {
        condition?.Invoke();
        Element.style.visibility = show ? Visibility.Visible : Visibility.Hidden;
    }
}
