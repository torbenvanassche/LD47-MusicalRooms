using UnityEngine.UIElements;

namespace UIE
{
    public class HoverButton : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<HoverButton, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            
        }

        public HoverButton()
        {
            focusable = true;
        }
    }
}
