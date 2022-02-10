using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace AntiProton.UIE
{
    public class ConditionalVisualElement : SelectableVisualElement
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
}
