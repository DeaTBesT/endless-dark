using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;

namespace Player
{
    public class DynamicInventoryDisplay : InventoryDisplay
    {
        [SerializeField] protected InventorySlotUI _slotUIPrefab;

        private void OnDisable()
        {
            if (_inventorySystem is not null)
            {
                _inventorySystem.OnInventorySlotChanged -= UpdateSlot;
            }
        }
        
        public void RefreshDynanicInventory(InventorySystem inventorySystem)
        {
            ClearSlots();
            
            _inventorySystem = inventorySystem;
            if (_inventorySystem is not null)
            {
                _inventorySystem.OnInventorySlotChanged += UpdateSlot;
            }
            
            AssignSlot(inventorySystem);
        }
        
        protected override void AssignSlot(InventorySystem inventoryToDisplay)
        {
            _slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

            if (inventoryToDisplay is null)
            {
                return;
            }
 
            for (int i = 0; i < inventoryToDisplay.GetInventorySize; i++)
            {
                var uiSlot = Instantiate(_slotUIPrefab, transform);
                _slotDictionary.Add(uiSlot, inventoryToDisplay.GetInventorySlots[i]);
                uiSlot.Initialize(inventoryToDisplay.GetInventorySlots[i]);
                uiSlot.UpdateUISlot();
            }
        }

        private void ClearSlots()
        {
            foreach (var item in transform.Cast<Transform>())
            {
                Destroy(item.gameObject);
            }
            
            if (_slotDictionary is not null)
            {
                _slotDictionary.Clear();
            }
        }
    }
}