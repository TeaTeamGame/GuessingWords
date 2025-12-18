using Network;
using UI;
using UnityEngine;
using UnityEngine.UIElements;
using GameMode = GameCore.GameMode;

namespace Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        [SerializeField] private StartGameObject startGameObject;
        [SerializeField] private GameModeController modeController;
        
        private RunnerCallback _runnerCallback;
        private GameMode _mode;

        private void Awake()
        {
            _runnerCallback = startGameObject.gameObject.AddComponent<RunnerCallback>();
            _runnerCallback.OnHostConnected += () =>
            {
                modeController.SetGameMode(_mode);
            };
        }

        private void Start()
        {
            var startGameUI = new StartGameUI();
            startGameUI.Inject(document);
            startGameUI.OnCreateGameBtnClicked += CreateGameHandle;
            startGameUI.OnJoinGameBtnClicked += JoinGameHandle;
            
            startGameUI.Open();
        }
        
        private void CreateGameHandle(GameMode mode)
        {
            // Start load
            _mode = mode;
            
            startGameObject.StartHost();
        }
        
        private void JoinGameHandle(string sessionName)
        {
            startGameObject.StartClient(sessionName);
        }
    }
}