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
        [SerializeField] private GameStateChangeEvent changeGameStateEvent;
        
        [SerializeField] private GameConfigController gameConfigController;
        [SerializeField] private RoomManager roomManager;

        private RunnerCallback _runnerCallback;
        private CreateGameConfig _createGameConfig;
        
        private void Start()
        {
            _runnerCallback = roomManager.GetComponent<RunnerCallback>();
            _runnerCallback.OnRoomCreated += () =>
            {
                gameConfigController.SetConfig(_createGameConfig);
            };
            
            changeGameStateEvent?.Raise(GameState.MainMenu);    
        }

        private void OnEnable()
        {
            createGameEvent.RegisterListener(CreateGameRequested);
        }

        private void OnDisable()
        {
            createGameEvent.UnregisterListener(CreateGameRequested);
        }

        private void CreateGameRequested(CreateGameConfig config)
        {
            _createGameConfig = config;
            roomManager.StartHost();
        }
    }
}