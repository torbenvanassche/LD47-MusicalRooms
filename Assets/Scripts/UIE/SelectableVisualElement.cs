using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIE
{
    public abstract class SelectableVisualElement : SerializedMonoBehaviour
    {
        [SerializeField] protected UIDocument target;
        [ValueDropdown(nameof(GetElements)), OnValueChanged(nameof(SetElement))]
        public string id;
    
        protected VisualElement element;

        protected abstract IEnumerable GetElements();
        protected abstract void Initialize();

        protected IEnumerable GetVisualElementsOfType<T>() where T : VisualElement
        {
            if (target)
            {
                var data = target.rootVisualElement.Query<T>().ToList()
                    .Select(visualElement => visualElement.name)
                    .Where(s => !string.IsNullOrEmpty(s));
                return data;
            }

            return null;
        }

        protected void SetElement()
        {
            element = target.rootVisualElement.Query<VisualElement>(id);
        }

        private void Awake()
        {
            //Failsafe set element
            if (element == null) SetElement();
            
            //If the element is not found, exit initialization
            if (element == null) return;
            Initialize();
        }
    }
}
