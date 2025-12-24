using Fusion;

namespace Gameplay.GameConfig
{
    public class GameConfigController : NetworkBehaviour
    {
        [Networked] public int NumberOfDigits { get; private set; }

        public void SetConfig(ConnectRoomArgs.CreateRoomArgs args)
        {
            if (!Runner.IsServer) return;
            
            NumberOfDigits = args.NumberOfDigits;
        }
    }
}