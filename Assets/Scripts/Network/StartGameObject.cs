using Fusion;
using Generals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public class StartGameObject : Singleton<StartGameObject>
    {
        private NetworkRunner _runner;
        
        private async void StartGame(StartGameArgs args)
        {
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
            Debug.Log("Start game finished");
        }

        public void StartHost()
        {
            StartGame(new StartGameArgs
            {
                GameMode = GameMode.Host,
            });
        }

        public void StartClient(string roomName)
        {
            StartGame(new StartGameArgs
            {
                GameMode = GameMode.Client,
                SessionName = roomName
            });
        }
    }
}
