using UnityEngine.UIElements;

namespace UI.CustomElement
{
    [UxmlElement]
    public partial class CustomButton : Button
    {
        public CustomButton()
        {
            RemoveFromClassList("unity-button");
            AddToClassList("custom-button");
        }
    }
}
