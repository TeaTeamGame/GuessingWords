using UnityEngine;

namespace GameCore.GameEvent
{
    [CreateAssetMenu(fileName = "GameStateChange", menuName = "Core/Game events/Game state change")]
    public class GameStateChangeEvent : GameEvent<GameState.GameState>
    {
        
    }
}