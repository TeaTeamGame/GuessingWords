using Fusion;
using Generals;
using UnityEngine.SceneManagement;

namespace Network
{
    public class StartGameObject : Singleton<StartGameObject>
    {
        private NetworkRunner _runner;

        private async void StartGame(GameMode mode)
        {
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;
            
            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid)
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }

            await _runner.StartGame(new StartGameArgs
            {
                GameMode = mode,
                Scene = scene,
                PlayerCount = 2,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }
    }
}
