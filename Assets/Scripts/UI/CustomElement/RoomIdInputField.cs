using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomElement
{
    [UxmlElement]
    public partial class RoomIdField : VisualElement
    {
        [UxmlAttribute]
        public int NumberOfDigits {
            get => _numberOfDigits;
            set
            {
                _numberOfDigits = value;
                UpdateField();
            }
            
        }
        
        private const string SlotClassName =  "room-id__slot";
        private const string SlotActiveClassName = "room-id__slot--active";
        
        private int _numberOfDigits;

        private Label[] _slots;
        private int _currentIndex;
        
        public RoomIdField()
        {
            AddToClassList("room-id");
        }

        public void SetDigit(int digit)
        {
            if (digit is < 0 or > 9) return;
            if (_currentIndex >= _numberOfDigits ||  _currentIndex < 0) return;
            
            var slot = _slots[_currentIndex];
            slot.text = digit.ToString();
            
            UpdateActiveSlot(_currentIndex + 1);
        }

        public bool TryGetValue(out string value)
        {
            value = "";
            if (_currentIndex != _numberOfDigits) return false;
            value = _slots.Aggregate("", (current, slot) => current + slot.text);
            return true;
        }

        public void Delete()
        {
            if (_currentIndex <= 0) return;
            
            UpdateActiveSlot(_currentIndex - 1);
            
            var deletedSlot = _slots[_currentIndex];
            if (deletedSlot != null) deletedSlot.text = "";
            
        }

        private void UpdateActiveSlot(int newIndex)
        {
            if (_currentIndex >= 0 && _currentIndex < _numberOfDigits)
            {
                var lastActiveSlot = _slots[_currentIndex];
                lastActiveSlot?.RemoveFromClassList(SlotActiveClassName);
            }
            
            _currentIndex = Mathf.Min(newIndex, _numberOfDigits);
            if (_currentIndex < 0 || _currentIndex >= _numberOfDigits) return;
            var newActiveSlot = _slots[_currentIndex];
            newActiveSlot?.AddToClassList(SlotActiveClassName);
        }

        private void UpdateField()
        {
            if (_numberOfDigits <= 0)
            {
                _numberOfDigits = 1;
            }

            Clear();
            _slots = new Label[_numberOfDigits];
            for (var i = 0; i < _numberOfDigits; i++)
            {
                var slot = new Label();
                slot.AddToClassList(SlotClassName);
                Add(slot);

                _slots[i] = slot;
            }
            
            UpdateActiveSlot(0);
        }
    }
}
