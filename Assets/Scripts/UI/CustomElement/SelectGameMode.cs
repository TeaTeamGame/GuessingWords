using GameCore;
using UnityEngine.UIElements;

namespace UI.CustomElement
{
    [UxmlElement]
    public partial class SelectGameMode : VisualElement
    {
        private Label _label;
        private Button _guessingWordButton;
        private Button _guessingNumberButton;

        public GameMode CurrentSelection { get; private set; }
        
        public SelectGameMode()
        {
            _label = new Label
            {
                text = "Game mode"
            };

            _guessingWordButton = new CustomButton { name = "GuessingWordButton", text = ""};
            _guessingWordButton.AddToClassList("game-mode-selection__border");
            _guessingWordButton.AddToClassList("game-mode-selection__word");
            
            _guessingNumberButton = new CustomButton { name = "GuessingWordButton", text = ""};
            _guessingNumberButton.AddToClassList("game-mode-selection__border");
            _guessingNumberButton.AddToClassList("game-mode-selection__number");

            Add(_label);
            Add(_guessingWordButton);
            Add(_guessingNumberButton);
            
            AddToClassList("text-element");
            AddToClassList("game-mode-selections");
            
            _guessingWordButton.clickable.clicked += () =>  SetMode(GameMode.GuessingWords);
            _guessingNumberButton.clickable.clicked += () =>  SetMode(GameMode.GuessingNumber);
            
            SetMode(GameMode.GuessingWords);
        }

        private void SetMode(GameMode mode)
        {
            CurrentSelection = mode;
            var ifWordMode = mode == GameMode.GuessingWords;
            _guessingWordButton.EnableInClassList("game-mode-selection--selected", ifWordMode);
            _guessingNumberButton.EnableInClassList("game-mode-selection--selected", !ifWordMode);
        }
    }
}