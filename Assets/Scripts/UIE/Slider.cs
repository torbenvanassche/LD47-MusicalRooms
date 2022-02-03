using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIE
{
    public class Slider : VisualElement
    {
        private VisualElement content;
        
        public new class UxmlFactory : UxmlFactory<Slider, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlIntAttributeDescription Progress =
                new () { name = "fill-percentage", defaultValue = 0 };
            
            UxmlColorAttributeDescription FillColor = 
                new () { name = "fill-color", defaultValue = Color.black };
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var s = ve as Slider;

                s.fillPercentage = Progress.GetValueFromBag(bag, cc);
                s.fillColor = FillColor.GetValueFromBag(bag, cc);
            }
        }

        public Slider()
        {
            focusable = true;
            AddToClassList("default-slider");

            content = new VisualElement
            {
                style =
                {
                    height = Length.Percent(100),
                    width = Length.Percent(fillPercentage)
                }
            };
            Add(content);
        }

        private int fill = 0;
        public int fillPercentage
        {
            get => fill;
            set
            {
                fill = Math.Clamp(value, 0, 100);
                content.style.width = Length.Percent(fill);
            }
        }
        
        private Color color = Color.black;
        private Color fillColor
        {
            get => color;
            set
            {
                content.style.backgroundColor = color;
                color = value;
            }
        }
    }
}
