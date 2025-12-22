using System;
using UnityEngine;

namespace GameCore.PlayerInfo
{
    public class PlayerInfoData
    {
        public Action OnPlayerNameUpdated;
        public Action OnPlayerAvatarUpdated;
        
        public string PlayerName { get; private set; }
        public Sprite PlayerAvatar { get; private set; }
        
        public void SetPlayerName(string name)
        {
            PlayerName = name;
            OnPlayerNameUpdated?.Invoke();
        }

        private void SetPlayerAvatar(Sprite avatar)
        {
            PlayerAvatar = avatar;
            OnPlayerAvatarUpdated?.Invoke();
        }
    }
}