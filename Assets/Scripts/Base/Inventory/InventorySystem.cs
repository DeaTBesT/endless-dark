using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    [System.Serializable]
    public class InventorySystem
    {
        [SerializeField] private List<InventorySlot> _inventorySlots;

        public List<InventorySlot> GetInventorySlots => _inventorySlots;
        public int GetInventorySize => _inventorySlots.Count;

        public UnityAction<InventorySlot> OnInventorySlotChanged;
        
        public InventorySystem(int size)
        {
            _inventorySlots = new List<InventorySlot>(size);

            for (int i = 0; i < size; i++)
            {
                _inventorySlots.Add(new InventorySlot());
            }
        }

        public bool AddToInventory(InventoryItemData itemData, int amount)
        {
            if (ContainsItem(itemData, out List<InventorySlot> inventorySlots))
            {
                foreach (var slot in inventorySlots)
                {
                    if (slot.IsEnoughtRoomLeftInStack(amount))
                    {
                        slot.AddToStack(amount);
                        OnInventorySlotChanged?.Invoke(slot);
                        return true;
                    }
                }
            }
            
            if (HasFreeSlot(out InventorySlot freeSlot))
            {
                if (freeSlot.IsEnoughtRoomLeftInStack(amount))
                {
                    freeSlot.UpdateInventorySlot(itemData, amount);
                    OnInventorySlotChanged?.Invoke(freeSlot);
                    return true;
                }
            }
            
            return false;
        }

        private bool ContainsItem(InventoryItemData item, out List<InventorySlot> slot)
        {
            slot = _inventorySlots.Where(x => x.GetItemData == item).ToList();
            return slot is not null;
        }

        private bool HasFreeSlot(out InventorySlot slot)
        {
            slot = _inventorySlots.FirstOrDefault(x => x.GetItemData is null);
            return slot is not null;
        }
    }
}