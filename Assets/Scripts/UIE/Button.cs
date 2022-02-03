using UnityEngine.UIElements;

namespace UIE
{
    public class Button : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<Button, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public Button()
        {
            focusable = true;
        }
    }
}
