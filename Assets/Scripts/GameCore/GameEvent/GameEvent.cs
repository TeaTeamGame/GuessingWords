using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.GameEvent
{
    [CreateAssetMenu(fileName = "Game event", menuName = "Core/Game events/Game event", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private Action _listeners;
        
        public void Raise()
        {
            _listeners?.Invoke();
        }
        
        public void RegisterListener(Action listener)
        {
            _listeners += listener;
        }
        
        public void UnregisterListener(Action listener)
        {
            _listeners -= listener;
        }
    }
    
    public abstract class GameEvent<T> : ScriptableObject
    {
        private Action<T> _listeners;

        public void Raise(T value)
        {
           _listeners?.Invoke(value);
        }
        
        public void RegisterListener(Action<T> listener)
        {
            _listeners += listener;
        }
        
        public void UnregisterListener(Action<T> listener)
        {
            _listeners -= listener;
        }
    }
}