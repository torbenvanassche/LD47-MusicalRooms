using System.Collections;
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
            return GetVisualElementsOfType<HoverButton>();
        }

        protected override void Initialize()
        {
            element?.RegisterCallback<PointerDownEvent>((evt) =>
            {
                pointerDownEvent.Invoke();
            });
            
            element?.RegisterCallback<PointerEnterEvent>((evt) =>
            {
                pointerEnterEvent.Invoke();
            });
            
            element?.RegisterCallback<PointerLeaveEvent>((evt) =>
            {
                pointerLeaveEvent.Invoke();
            });
        }
    }
}
