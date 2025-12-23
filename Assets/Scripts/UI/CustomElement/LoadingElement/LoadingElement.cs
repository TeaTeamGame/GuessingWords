using System;
using UnityEngine.UIElements;

namespace UI.CustomElement.LoadingElement
{
    public class LoadingElement 
    {
        private readonly LoadingEffectElement _loadingEffect;
        private readonly VisualElement _loadingVisualElement;
        
        public LoadingElement(VisualElement element)
        {
            _loadingVisualElement = element;
            _loadingEffect = element.Q<LoadingEffectElement>();
        }
        
        public void AddProcess(float delta)
        {
            _loadingEffect.AddProcess(delta);
        }

        public void Show()
        {
            _loadingVisualElement.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _loadingVisualElement.style.display = DisplayStyle.None;
        }
        
    }
}