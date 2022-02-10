using UnityEngine.UIElements;

namespace AntiProton.UIE
{
    public abstract class DraggableVisualElement : VisualElement
    {
        protected DraggableVisualElement()
        {
            RegisterCallback<PointerDownEvent>(DragStartHandler);
            RegisterCallback<PointerUpEvent>(DragLeavePointerUp);
            RegisterCallback<PointerLeaveEvent>(DragLeavePointerExited);
        }

        protected abstract void OnDrag(PointerMoveEvent evt);

        private void DragStartHandler(PointerDownEvent evt)
        {
            RegisterCallback<PointerMoveEvent>(OnDrag);
        }

        private void DragLeavePointerExited(PointerLeaveEvent evt)
        {
            UnregisterCallback<PointerMoveEvent>(OnDrag);
        }

        private void DragLeavePointerUp(PointerUpEvent evt)
        {
            UnregisterCallback<PointerMoveEvent>(OnDrag);
        }
    }
}
