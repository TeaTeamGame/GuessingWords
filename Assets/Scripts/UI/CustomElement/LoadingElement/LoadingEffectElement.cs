using UnityEngine.UIElements;

namespace UI.CustomElement.LoadingElement
{
    public abstract partial class LoadingEffectElement : VisualElement
    {
        public abstract void AddProcess(float delta);
    }
}