using Fusion;

namespace Gameplay.GameConfig
{
    public class GameConfigController : NetworkBehaviour
    {
        [Networked] public int NumberOfDigits { get; private set; }

        public void SetConfig(CreateGameConfig config)
        {
            if (!Runner.IsServer) return;
            
            NumberOfDigits = config.NumberOfDigits;
        }
    }
}