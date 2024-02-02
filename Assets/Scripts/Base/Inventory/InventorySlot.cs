using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] private InventoryItemData _itemData;
        [SerializeField] private int _stackSize;
        
        public InventoryItemData GetItemData => _itemData;
        public int GetStackSize => _stackSize;

        public InventorySlot(InventoryItemData itemData, int stackSize)
        {
            _itemData = itemData;
            _stackSize = stackSize;
        }

        public InventorySlot()
        {
            ClearSlot();
        }

        public void ClearSlot()
        {
            _itemData = null;
            _stackSize = -1;
        }

        public void AssignItem(InventorySlot slot)
        {
            if (_itemData == slot.GetItemData)
            {
                AddToStack(slot.GetStackSize);
            }
            else
            {
                _itemData = slot.GetItemData;
                _stackSize = 0;
                AddToStack(slot.GetStackSize);
            }
        }
        
        public void UpdateInventorySlot(InventoryItemData itemData, int amount)
        {
            _itemData = itemData;
            _stackSize = amount;
        }
        
        public bool TryToRoomLeftInStack(int amountToAdd, out int amountRemaining)
        {
            amountRemaining = _itemData.GetMaxStackSize - _stackSize;

            return IsEnoughtRoomLeftInStack(amountToAdd);
        }

        public bool IsEnoughtRoomLeftInStack(int amountToAdd)
        {
            if ((_itemData is null) || (_itemData is not null) && 
                (_stackSize + amountToAdd <= _itemData.GetMaxStackSize))
            {
                return true;
            }

            return false;
        }

        public void AddToStack(int amount)
        {
            _stackSize += amount;
        }

        public void RemoveFromStack(int amount)
        {
            _stackSize -= amount;
        }

        public bool SplitStack(out InventorySlot splitStack)
        {
            if (_stackSize <= 1)
            {
                splitStack = null;
                return false;
            }
            
            int halfStack = Mathf.RoundToInt(_stackSize / 2);
            RemoveFromStack(halfStack);

            splitStack = new InventorySlot(_itemData, halfStack);
            return true;
        }
    }
}