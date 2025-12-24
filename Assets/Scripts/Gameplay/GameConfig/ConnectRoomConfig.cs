namespace Gameplay.GameConfig
{
    public class ConnectRoomArgs
    {
        public struct CreateRoomArgs
        {
            public int NumberOfDigits;
        }

        public struct JoinRoomArgs
        {
            public string RoomCode;
        }
    }
}