using Inventory;
using UnityEngine;

namespace Player
{
    public class StaticInventoryDisplay : InventoryDisplay
    {
        [SerializeField] private InventoryHolder _playerInventory;
        [SerializeField] private InventorySlotUI[] _slots;

        protected override void Start()
        {
            base.Start();

            if (_playerInventory != null)
            {
                _inventorySystem = _playerInventory.GetInventorySystem;
                _inventorySystem.OnInventorySlotChanged += UpdateSlot;
            }
            else
            {
                Debug.LogWarning($"No inventory assigned to {gameObject}");
            }

            AssignSlot(_inventorySystem);
        }

        protected override void AssignSlot(InventorySystem inventoryToDisplay)
        {
            _slotDictionary = new();

            if (_slots.Length != _inventorySystem.GetInventorySize)
            {
                Debug.LogWarning($"Inventory slots out of sync on {gameObject}");
            }

            for (int i = 0; i < _inventorySystem.GetInventorySize; i++)
            {
                _slotDictionary.Add(_slots[i], _inventorySystem.GetInventorySlots[i]);
                _slots[i].Initialize(_inventorySystem.GetInventorySlots[i]);
            }
        }
    }
}