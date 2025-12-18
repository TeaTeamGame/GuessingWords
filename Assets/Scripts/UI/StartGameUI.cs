using System;
using Network;
using UI.CustomElement;
using UnityEngine;
using UnityEngine.UIElements;
using GameMode = GameCore.GameMode;

namespace UI
{
    public class StartGameUI
    {
        public Action<GameMode> OnCreateGameBtnClicked;
        public Action<string> OnJoinGameBtnClicked;

        private VisualElement _root;
        
        private VisualElement _selectModeMenu;
        private Button _createModeButton;
        private Button _joinModeButton;
        
        private VisualElement _createGameMenu;
        private SelectGameMode _selectGameMode;
        private Button _createGameButton;

        private VisualElement _joinModeMenu;
        private TextField _joinGameCodeField;
        private Button _joinGameButton;

        private StartGameUIState _currentState = StartGameUIState.None;
        
        public void Inject(UIDocument uiDocument)
        {
            _root = uiDocument.rootVisualElement;
            
            // Select mode menu
            _selectModeMenu = _root.Q<VisualElement>("start-game-options");
            _createModeButton = _selectModeMenu.Q<Button>("create-game-btn");
            _joinModeButton = _selectModeMenu.Q<Button>("join-game-btn");
            
            // Create game menu
            _createGameMenu = _root.Q<VisualElement>("create-game-ui");
            _selectGameMode = _createGameMenu.Q<SelectGameMode>();
            _createGameButton = _createGameMenu.Q<Button>("create-game-btn");
            
            // Join game menu
            _joinModeMenu = _root.Q<VisualElement>("join-game-ui");
            _joinGameCodeField = _joinModeMenu.Q<TextField>("join-game-code-field");
            _joinGameButton = _joinModeMenu.Q<Button>("join-game-btn");
        }
        
        public void Open()
        {
            Debug.Log("Open StartGameUI");
            ChangeState(StartGameUIState.ModeSelection);
        }
        
        private void ChangeState(StartGameUIState state)
        {
            if (state == _currentState || state == StartGameUIState.None) return;
            switch (_currentState)
            {
                case StartGameUIState.ModeSelection:
                    _selectModeMenu.style.display = DisplayStyle.None;
                    _createModeButton.clickable.clicked -= OnCreateButtonClicked;
                    _joinModeButton.clickable.clicked -= OnJoinButtonClicked;
                    
                    break;
                case StartGameUIState.CreateMode:
                    _createGameMenu.style.display = DisplayStyle.None;
                    _createGameButton.clickable.clicked -= CreateGame;
                    break;
                case StartGameUIState.JoinMode:
                    _joinModeMenu.style.display = DisplayStyle.None;
                    _joinGameButton.clickable.clicked -= JoinGame;
                    break;
                case StartGameUIState.None:
                default:
                    _selectModeMenu.style.display = DisplayStyle.None;
                    _createGameMenu.style.display = DisplayStyle.None;
                    _joinModeMenu.style.display = DisplayStyle.None;

                    break;
            }

            _currentState = state;
            switch (_currentState)
            {
                case StartGameUIState.ModeSelection:
                    _selectModeMenu.style.display = DisplayStyle.Flex;
                
                    _createModeButton.clickable.clicked += OnCreateButtonClicked;
                    _joinModeButton.clickable.clicked += OnJoinButtonClicked;
                    break;
                case StartGameUIState.CreateMode:
                    _createGameMenu.style.display = DisplayStyle.Flex;
                
                    _createGameButton.clickable.clicked += CreateGame;
                    break;
                case StartGameUIState.JoinMode:
                    _joinModeMenu.style.display = DisplayStyle.Flex;
                    _joinGameButton.clickable.clicked += JoinGame;
                    break;
                case StartGameUIState.None:
                default:
                    break;
            }
        }

        private void OnCreateButtonClicked()
        {
            ChangeState(StartGameUIState.CreateMode);
        }

        private void OnJoinButtonClicked()
        {
            ChangeState(StartGameUIState.JoinMode);
        }

        private void CreateGame()
        {

            OnCreateGameBtnClicked?.Invoke(_selectGameMode.CurrentSelection);
        }

        private void JoinGame()
        {
            OnJoinGameBtnClicked?.Invoke(_joinGameCodeField.text);
        }

        private enum StartGameUIState
        {
            None,
            ModeSelection,
            CreateMode,
            JoinMode
        }
    }
}
