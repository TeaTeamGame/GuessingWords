using System;
using System.Collections.Generic;
using GameCore.GameEvent;
using GameCore.GameState;
using UI.Pages;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Serializable]
        public class UiPage
        {
            public PageName pageName;
            public UIPageBase page;
        }
        
        private struct PageNavigation
        {
            public PageName From;
            public PageName To;
        }
        
        [SerializeField] private GameStateChangeEvent changeGameStateEvent;
        [SerializeField] private GameEvent backToPreviousPageEvent;
        
        [SerializeField] private UiPage[] pageObjects;

        private Dictionary<PageName, IUIPage> _pageDictionary = new ();
        private PageName _currentPage;
        
        private Stack<PageNavigation> _pageHistory = new ();
        
        public void Awake()
        {
            foreach (var uiPage in pageObjects)
            {
                _pageDictionary[uiPage.pageName] = uiPage.page;
                uiPage.page.Hide();
            }

            pageObjects = null;
        }

        private void OnEnable()
        {
            changeGameStateEvent.RegisterListener(OnGameStateChanged);
            backToPreviousPageEvent.RegisterListener(BackToPreviousPage);
        }

        private void OnDisable()
        {
            changeGameStateEvent.UnregisterListener(OnGameStateChanged);
            backToPreviousPageEvent.UnregisterListener(BackToPreviousPage);
        }
        
        private void BackToPreviousPage()
        {
            if (_pageHistory.Count == 0) return;

            var lastNavigation = _pageHistory.Pop();
            SwitchPage(lastNavigation.From, false);
        }

        private void OnGameStateChanged(GameState value)
        {
            switch (value)
            {
                case GameState.MainMenu:
                    SwitchPage(PageName.HomePage);
                    break;
                case GameState.CreateRoom:
                    SwitchPage(PageName.CreateRoomPage);
                    break;
                case GameState.JoinRoom:
                    SwitchPage(PageName.JoinRoomPage);
                    break;
                case GameState.WaitingForOpponent:
                    break;
                case GameState.EnterSecret:
                    break;
                case GameState.InGame:
                    break;
                case GameState.EndGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        private void SwitchPage(PageName nextPage, bool writeHistory = true)
        {
            if (_currentPage == nextPage || nextPage == PageName.None) return;
            
            _pageDictionary[nextPage].Show();

            if (_currentPage != PageName.None)
            {
                _pageDictionary[_currentPage].Hide();
                if (writeHistory)
                {
                    _pageHistory.Push(new PageNavigation
                    {
                        From = _currentPage,
                        To = nextPage
                    });
                }
            }
            
            _currentPage = nextPage;
        }
    }
}