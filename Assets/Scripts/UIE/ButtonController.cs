using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UIE
{
    public class ButtonController : SelectableVisualElement
    {
        public UnityEvent pointerDownEvent;
        public UnityEvent pointerEnterEvent;
        public UnityEvent pointerLeaveEvent;

        protected override IEnumerable GetElements()
        {
            return GetVisualElementsOfType<Button>();
        }

        protected void Awake()
        {
            Element.RegisterCallback<PointerDownEvent>((evt) =>
            {
                pointerDownEvent.Invoke();
            });
            
            Element.RegisterCallback<PointerEnterEvent>((evt) =>
            {
                pointerEnterEvent.Invoke();
            });
            
            Element.RegisterCallback<PointerLeaveEvent>((evt) =>
            {
                pointerLeaveEvent.Invoke();
            });
        }
    }
}
