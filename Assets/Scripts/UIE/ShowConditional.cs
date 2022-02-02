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

    public void CheckWebGL()
    {
        show = Application.platform == RuntimePlatform.WebGLPlayer;
    }

    protected override void Initialize()
    {
        condition?.Invoke();
        element.style.visibility = show ? Visibility.Visible : Visibility.Hidden;
    }
}
