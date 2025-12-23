using Gameplay.GameConfig;
using UnityEngine;

namespace GameCore.GameEvent
{
    [CreateAssetMenu(fileName = "CreateGameEvent", menuName = "Core/Game events/Create game event")]
    public class CreateGameEvent : GameEvent<CreateGameConfig>
    {
    }
}