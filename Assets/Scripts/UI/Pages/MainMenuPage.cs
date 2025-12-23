using GameCore.GameEvent;
using GameCore.GameState;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Pages
{
    public class MainMenuPage : UIPageBase
    {
        [SerializeField] private GameStateChangeEvent gameStateChangeEvent;
        
        private Button _createRoomButton;
        private Button _joinRoomButton;
        
        protected override void Setup()
        {
            _createRoomButton = RootElement.Q<Button>("create-game-btn");
            _joinRoomButton = RootElement.Q<Button>("join-game-btn");
        }

        protected override void OnShow()
        {
            _createRoomButton.clicked += OnCreateRoomClicked;
            _joinRoomButton.clicked += OnJoinRoomClicked;
        }

        protected override void OnHide()
        {
            _createRoomButton.clicked -= OnCreateRoomClicked;
            _joinRoomButton.clicked -= OnJoinRoomClicked;
        }
        
        private void OnCreateRoomClicked()
        {
            gameStateChangeEvent?.Raise(GameState.CreateRoom);
        }
        
        private void OnJoinRoomClicked()
        {
            gameStateChangeEvent?.Raise(GameState.JoinRoom);
        }
    }
}