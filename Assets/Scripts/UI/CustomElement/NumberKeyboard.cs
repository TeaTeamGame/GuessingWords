using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomElement
{
    [UxmlElement]
    public partial class NumberKeyboard : VisualElement
    {
        [UxmlAttribute]
        private string ConfirmButtonText
        {
            get => _confirmButtonText;
            set
            {
                _confirmButtonText = value;
                SetConfirmButtonText();
            }
        }

        private void SetConfirmButtonText()
        {
            _confirmButton.text = _confirmButtonText;
        }

        private string _confirmButtonText = "Confirm";

        [UxmlAttribute]
        private Texture2D DeleteIcon
        {
            get => _deleteIcon;
            set
            {
                _deleteIcon = value;
                SetDeleteButtonIcon();
            }
        }

        private void SetDeleteButtonIcon()
        {
            if (_deleteIcon == null) return;
            var iconImage = new Background
            {
                texture = _deleteIcon
            };
            _deleteButton.iconImage = iconImage;
        }

        private Texture2D _deleteIcon;
        
        public Action<int> OnNumberKeyPressed;
        public Action ConfirmButtonPressed;
        public Action DeleteButtonPressed;

        private const string KeyboardRowClassName = "number-keyboard__row";
        private const string NumberButtonClassName = "number-keyboard__number-button";
        private const string DeleteButtonClassName = "number-keyboard__delete-button";
        private const string ConfirmButtonClassName = "number-keyboard__confirm-button";
        
        private Button _deleteButton;
        private Button _confirmButton;
        
        public NumberKeyboard()
        {
            var rows = new VisualElement[4];
            
            for (var i = 0; i < 4; i++)
            {
                rows[i] = new VisualElement();
                rows[i].AddToClassList(KeyboardRowClassName);
                Add(rows[i]);
            }

            for (var i = 1; i <= 9; i++)
            {
                var value = i;
                AddButton(rows[(i - 1) / 3], NumberButtonClassName, () => OnNumberKeyPressed?.Invoke(value), i.ToString());
            }

            _deleteButton = AddButton(rows[3], DeleteButtonClassName, () => DeleteButtonPressed?.Invoke(), "");
            AddButton(rows[3], NumberButtonClassName, () => OnNumberKeyPressed?.Invoke(0), "0");
            _confirmButton = AddButton(rows[3], ConfirmButtonClassName, () => ConfirmButtonPressed?.Invoke(), ConfirmButtonText);
            return;

            Button AddButton(VisualElement container, string className, Action action, string buttonText = "")
            {
                var newButton = new CustomButton();
                if (!string.IsNullOrEmpty(buttonText))
                {
                    newButton.text = buttonText;
                }
                
                newButton.AddToClassList(className);
                newButton.clickable.clicked += action;
                container.Add(newButton);

                return newButton;
            } 
        }
    }
}