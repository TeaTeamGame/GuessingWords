using System.Collections;
using GameCore.GameEvent;
using UI.CustomElement;
using UI.CustomElement.LoadingElement;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Pages
{
    public class JoinRoomPage : UIPageBase
    {
        [SerializeField] private GameEvent<string> requestToJoinRoomEvent;
        [SerializeField] private GameEvent uiLoadingEvent;
        [SerializeField] private GameEvent backToPreviousPageEvent;
        
        [SerializeField] private float effectSpeed = 2f;
        
        private Button _backButton;
        private RoomIdField _roomIdField;
        private NumberKeyboard _numberKeyboard;
        private LoadingElement _loadingElement;
        
        private bool _isJoining;

        protected override void Setup()
        {
            _backButton = RootElement.Q<Button>("back-btn");

            _roomIdField = RootElement.Q<RoomIdField>();
            _numberKeyboard = RootElement.Q<NumberKeyboard>();
            
            var loadingVisualElement = RootElement.Q<VisualElement>("loading-element");
            _loadingElement = new LoadingElement(loadingVisualElement);
        }

        protected override void OnShow()
        {
            _backButton.clickable.clicked += backToPreviousPageEvent.Raise;
            _numberKeyboard.ConfirmButtonPressed += ConfirmButtonPressed;
            _numberKeyboard.OnNumberKeyPressed += OnNumberKeyPressed;
            _numberKeyboard.DeleteButtonPressed += OnDeleteButtonPressed;
            
            uiLoadingEvent.RegisterListener(OnLoadingEvent);
        }
        
        protected override void OnHide()
        {
            _isJoining = false;
            StopAllCoroutines();
            
            _backButton.clickable.clicked -= backToPreviousPageEvent.Raise;
            _numberKeyboard.ConfirmButtonPressed -= ConfirmButtonPressed;
            _numberKeyboard.OnNumberKeyPressed -= OnNumberKeyPressed;
            _numberKeyboard.DeleteButtonPressed -= OnDeleteButtonPressed;
        }

        private void ConfirmButtonPressed()
        {
            // TODO: Implement room joining logic here
            // get room id from _roomIdField and attempt to join the room
            if (_roomIdField.TryGetValue(out var roomId))
            {
                requestToJoinRoomEvent?.Raise(roomId);
            }
        }

        private void OnDeleteButtonPressed()
        {
            _roomIdField.Delete();
        }

        private void OnNumberKeyPressed(int value)
        {
            _roomIdField.SetDigit(value);
        }
        
        private void OnLoadingEvent()
        {
            _loadingElement.Show();
            _numberKeyboard.style.display = DisplayStyle.None;
            StartCoroutine(EJoining());
        }
        
        private IEnumerator EJoining()
        {
            _isJoining = true;
            yield return new WaitWhile(() =>
            {
                _loadingElement.AddProcess(Time.deltaTime * effectSpeed);
                return _isJoining;
            });
        }
    }
}