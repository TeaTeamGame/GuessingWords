using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class StartGameUI : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        private VisualElement _root;
        private Button _createButton;
        private Button _joinButton;
        
        private void Awake()
        {
            _root = uiDocument.rootVisualElement;
            var startGameMenu = _root.Q<VisualElement>("start-game-options");
            _createButton = startGameMenu.Q<Button>("create-game-btn");
            _joinButton = startGameMenu.Q<Button>("join-game-btn");
        }

        private void OnEnable()
        {
            _createButton.clickable.clicked += OnCreateButtonClicked;
            _joinButton.clickable.clicked += OnJoinButtonClicked;
        }

        private void OnDisable()
        {
            _createButton.clickable.clicked -= OnCreateButtonClicked;
            _joinButton.clickable.clicked -= OnJoinButtonClicked;
        }

        private void OnCreateButtonClicked()
        {
            // Open create game UI, include option for game mode
        }

        private void OnJoinButtonClicked()
        {
            // Open join game UI, include option for entering room code
        }
    }
}
