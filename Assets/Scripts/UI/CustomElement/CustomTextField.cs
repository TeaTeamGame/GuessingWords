using UnityEngine.UIElements;

namespace UI.CustomElement
{
    [UxmlElement]
    public partial class CustomTextField : TextField
    {
        public CustomTextField()
        {
            RemoveFromClassList("unity-text-field");
            AddToClassList("custom-text-field");
            
            labelElement.RemoveFromClassList("unity-text-field__label");
            labelElement.RemoveFromClassList("unity-base-field__label");
        }
    }
}
