using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using GameCore.GameEvent;
using Generals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Network
{
    public class RoomManager : Singleton<RoomManager>, INetworkRunnerCallbacks
    {
        [SerializeField] private GameEvent loadingEvent;
     
        public Action OnRoomCreated;
        
        private NetworkRunner _runner;
        private readonly HashSet<string> _availableRooms = new ();
        private bool _isSessionListUpdated;

        private async Task StartGame(StartGameArgs args)
        {
            loadingEvent.Raise();
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            _isSessionListUpdated = false;

            if (args.GameMode == GameMode.Host)
            {
                await _runner.JoinSessionLobby(SessionLobby.ClientServer);
                while (!_isSessionListUpdated)
                {
                    await Task.Yield();
                }
                
                //  choose a random room name that is not already taken
                string roomName;
                do
                {
                    roomName = Random.Range(1, 1000000).ToString("D6");
                } while (_availableRooms.Contains(roomName));

                args.SessionName = roomName;
            }
            
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
        
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
        {
            print($"Session id:  {runner.SessionInfo.Name}");

            if (player == runner.LocalPlayer && runner.IsServer)
            {
                OnRoomCreated?.Invoke();
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            // TODO: Handle start host fail
            // TODO: Handle server shutdown
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            // TODO: Handle client disconnect
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token) { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            // TODO: Handle client connect fail
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

        public void OnInput(NetworkRunner runner, NetworkInput input) { }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        public void OnConnectedToServer(NetworkRunner connectedRunner)
        {
            print("OnConnectedToServer");
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            print("OnSessionListUpdated");
            foreach (var sessionInfo in sessionList)
            {
                _availableRooms.Add(sessionInfo.Name);
            }

            _isSessionListUpdated = true;
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        public void OnSceneLoadDone(NetworkRunner runner) { }

        public void OnSceneLoadStart(NetworkRunner runner) { }
    }
}
