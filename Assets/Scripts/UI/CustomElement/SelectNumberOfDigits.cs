using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomElement
{
    [UxmlElement]
    public partial class SelectNumberOfDigits : VisualElement
    {
        [UxmlAttribute]
        public List<int> NumberOfDigits
        {
            get => _numberOfDigitsOptions;
            set
            {
                _numberOfDigitsOptions = value;
                DisplayOptions();
                MarkDirtyRepaint();
            }
        }
        
        private List<int> _numberOfDigitsOptions =  new(){ 4, 5, 6 };

        private const string PrefixClass = "select-number-of-digits";
        private const string ActiveOptionClass = "option--active";
        
        private readonly VisualElement _optionsContainer;
        private readonly Label _descriptionLabel;
        
        public int CurrentSelectedValue { get; private set; }
        private Button _currentActiveOption;
        
        public SelectNumberOfDigits()
        {
            var label = new Label("Số lượng chữ số");
            label.AddToClassList($"{PrefixClass}__label");
            Add(label);

            _optionsContainer = new VisualElement { name = "options-container" };
            _optionsContainer.AddToClassList($"{PrefixClass}__options-container");
            Add(_optionsContainer);

            _descriptionLabel = new Label();
            _descriptionLabel.AddToClassList($"{PrefixClass}__description-label");
            Add(_descriptionLabel);

            DisplayOptions();
        }

        private void DisplayOptions()
        {
            if (_optionsContainer == null) return;
            
            _optionsContainer.Clear();
            _currentActiveOption = null;
            CurrentSelectedValue = 0;
            
            var sortedOptions = _numberOfDigitsOptions.OrderBy(a => a).ToList();
            var defaultSelected = (_numberOfDigitsOptions.Count - 1) / 2;
            for (var i = 0;  i < sortedOptions.Count; i++)
            {
                var optionValue = sortedOptions[i];
                var optionButton = new CustomButton { text = $"{optionValue} số" };
                optionButton.AddToClassList($"{PrefixClass}__option-button");
                optionButton.AddToClassList("button");
                optionButton.clickable.clicked += () =>
                {
                    SelectOption(optionButton, optionValue);
                };
                
                _optionsContainer.Add(optionButton);
                if (i == defaultSelected)
                {
                    SelectOption(optionButton, optionValue);
                }
            }
        }

        private void SelectOption(Button button, int option)
        {
            if (option == CurrentSelectedValue) return;
            
            _currentActiveOption?.RemoveFromClassList(ActiveOptionClass);
            
            _currentActiveOption = button;
            CurrentSelectedValue = option;
            _currentActiveOption.AddToClassList(ActiveOptionClass);
            
            _descriptionLabel.text = $"Người chơi phải đoán đúng dãy số bí mật gồm {CurrentSelectedValue} chữ số.";
        }
    }
}
