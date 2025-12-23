using System.Collections;
using GameCore.GameEvent;
using Gameplay.GameConfig;
using UI.CustomElement;
using UI.CustomElement.LoadingElement;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Pages
{
    public class CreateRoomPage : UIPageBase
    {
        [SerializeField] private GameEvent startCreateRoomEvent;
        [SerializeField] private CreateGameEvent requestCreateRoomEvent;
        [SerializeField] private GameEvent backToPreviousPageEvent;
        
        [SerializeField] private float effectSpeed = 0.3f;
        
        private Button _backButton;
        private SelectNumberOfDigits _numberOfDigits;
        private Button _createRoomButton;
        private LoadingElement _loadingElement;

        private bool _isCreating;
        
        protected override void Setup()
        {
            _backButton = RootElement.Q<Button>("back-btn");
            _createRoomButton = RootElement.Q<Button>("create-room-btn");
            _numberOfDigits = RootElement.Q<SelectNumberOfDigits>();

            var loadingVisualElement = RootElement.Q<VisualElement>("loading-element");
            _loadingElement = new LoadingElement(loadingVisualElement);
        }

        protected override void OnShow()
        {
            _loadingElement.Hide();
            
            startCreateRoomEvent.RegisterListener(OnLoadingEvent);
            _createRoomButton.clickable.clicked += CreateRoomButtonClicked;
            _backButton.clickable.clicked += OnBackButtonClicked;
        }

        protected override void OnHide()
        {
            _isCreating = false;
            StopAllCoroutines();
            
            startCreateRoomEvent.UnregisterListener(OnLoadingEvent);
            _createRoomButton.clickable.clicked -= CreateRoomButtonClicked;
            _backButton.clickable.clicked -= OnBackButtonClicked;
        }
        
        private void CreateRoomButtonClicked()
        {
            requestCreateRoomEvent.Raise(new CreateGameConfig
            {
                NumberOfDigits = _numberOfDigits.CurrentSelectedValue
            });
        }
        
        private void OnBackButtonClicked()
        {
            backToPreviousPageEvent.Raise();
        }

        private void OnLoadingEvent()
        {
            _loadingElement.Show();
            _createRoomButton.style.display = DisplayStyle.None;
            StartCoroutine(ECreating());
        }

        private IEnumerator ECreating()
        {
            _isCreating = true;
            yield return new WaitWhile(() =>
            {
                _loadingElement.AddProcess(Time.deltaTime * effectSpeed);
                return _isCreating;
            });
        }
    }
}