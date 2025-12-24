using System.Collections;
using GameCore.GameEvent;
using GameCore.GameState;
using Gameplay.GameConfig;
using Network;
using UnityEngine;

namespace Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CreateGameEvent createGameEvent;
        [SerializeField] private GameEvent<string> joinGameEvent;
        [SerializeField] private GameStateChangeEvent changeGameStateEvent;
        
        [SerializeField] private GameConfigController gameConfigController;
        [SerializeField] private RoomManager roomManager;
        
        private ConnectRoomArgs.CreateRoomArgs _createRoomArgs;
        
        private IEnumerator Start()
        {
            roomManager.OnRoomCreated += () =>
            {
                gameConfigController.SetConfig(_createRoomArgs);
            };

            yield return new WaitForEndOfFrame();
            
            changeGameStateEvent?.Raise(GameState.MainMenu);    
        }

        private void OnEnable()
        {
            createGameEvent.RegisterListener(HandleCreateGameRequest);
            joinGameEvent.RegisterListener(HandleJoinGameRequest);
        }

        private void OnDisable()
        {
            createGameEvent.UnregisterListener(HandleCreateGameRequest);
            joinGameEvent.UnregisterListener(HandleJoinGameRequest);

        }

        private void HandleCreateGameRequest(ConnectRoomArgs.CreateRoomArgs args)
        {
            _createRoomArgs = args;
            roomManager.StartHost();
        }
        
        private void HandleJoinGameRequest(string roomId)
        {
            roomManager.StartClient(roomId);
        }
    }
}