using System.Threading.Tasks;
using Fusion;
using GameCore.GameEvent;
using Generals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public class RoomManager : Singleton<RoomManager>
    {
        [SerializeField] private GameEvent loadingEvent;
        
        private NetworkRunner _runner;
        
        private async Task StartGame(StartGameArgs args)
        {
            loadingEvent.Raise();
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;
            
            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid)
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }

            args.PlayerCount = 2;
            args.Scene = scene;
            args.SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();
            
            await _runner.StartGame(args);
        }

        public async void StartHost()
        {
            await StartGame(new StartGameArgs
            {
                GameMode = GameMode.Host,
            });
        }

        public async void StartClient(string roomName)
        {
            await StartGame(new StartGameArgs
            {
                GameMode = GameMode.Client,
                SessionName = roomName
            });
        }
    }
}
