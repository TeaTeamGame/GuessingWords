using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Pages
{
    public abstract class UIPageBase : MonoBehaviour, IUIPage
    {
        [SerializeField] private UIDocument document;

        protected VisualElement RootElement;

        private void Awake()
        {
            RootElement = document.rootVisualElement;
            Setup();
        }

        public void Show()
        {
            OnShow();
            RootElement.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            OnHide();
            RootElement.style.display = DisplayStyle.None;
        }
        
        protected virtual void Setup() { }

        protected virtual void OnShow() { }

        protected virtual void OnHide() { }
    }
}