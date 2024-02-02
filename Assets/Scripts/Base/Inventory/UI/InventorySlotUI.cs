using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image _itemSprite;
        [SerializeField] private TextMeshProUGUI _textItemAmount;
        [SerializeField] private InventorySlot _assignedInventorySlot;

        private Button _button;

        public InventorySlot GetAssignedInventorySlot => _assignedInventorySlot;
        public InventoryDisplay ParentDisplay { get; private set; }

        private void Awake()
        {
            ClearSlot();
            
            TryGetComponent(out Button _button);
            _button?.onClick.AddListener(OnUISlotClick);

            transform.parent.TryGetComponent(out InventoryDisplay display);
            ParentDisplay = display;
        }

        public void Initialize(InventorySlot slot)
        {
            _assignedInventorySlot = slot;
            UpdateUISlot(slot);
        }

        public void UpdateUISlot(InventorySlot slot)
        {
            if (slot.GetItemData is not null)
            {
                _itemSprite.sprite = slot.GetItemData.GetIcon;
                _itemSprite.color = Color.white;
                _textItemAmount.text = slot.GetStackSize > 1 ? slot.GetStackSize.ToString() : string.Empty;
            }
            else
            {
                _itemSprite.sprite = null;
                _itemSprite.color = Color.clear;
                _textItemAmount.text = string.Empty;
            }
        }

        public void UpdateUISlot()
        {
            if (_assignedInventorySlot != null)
            {
                UpdateUISlot(_assignedInventorySlot);
            }
        }

        public void ClearSlot()//Bug
        {
            _assignedInventorySlot?.ClearSlot();
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;
            _textItemAmount.text = string.Empty;
        }

        private void OnUISlotClick()
        {
            ParentDisplay?.SlotClicked(this);
        }
    }
}