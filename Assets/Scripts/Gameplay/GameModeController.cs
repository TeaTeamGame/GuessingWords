using Fusion;
using GameMode = GameCore.GameMode;

namespace Gameplay
{
    public class GameModeController : NetworkBehaviour
    {
        [Networked]
        public GameMode CurrentGameMode { get; private set; }
        
        public void SetGameMode(GameMode mode)
        {
            if (Runner.IsServer)
                CurrentGameMode = mode;
        }
    }
}