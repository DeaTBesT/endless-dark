using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public abstract class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private MouseItemData _mouseInventoryItem;

        protected InventorySystem _inventorySystem;
        protected Dictionary<InventorySlotUI, InventorySlot> _slotDictionary;

        public InventorySystem GetInventorySystem => _inventorySystem;
        public Dictionary<InventorySlotUI, InventorySlot> GetSlotDictionary => _slotDictionary;
        
        protected abstract void AssignSlot(InventorySystem inventoryToDisplay);

        //Add initialize and deinitialize
        protected virtual void Start()
        {
        }

        protected virtual void UpdateSlot(InventorySlot updatedSlot)
        {
            foreach (var slot in _slotDictionary)
            {
                if (slot.Value == updatedSlot)
                {
                    slot.Key.UpdateUISlot(updatedSlot);
                }
            }
        }

        public void SlotClicked(InventorySlotUI clickedSlotUI)
        {
            //Rewrite input system
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

            if ((clickedSlotUI.GetAssignedInventorySlot.GetItemData is not null) &&
                (_mouseInventoryItem.AssingedInventorySlot.GetItemData is null))
            {
                if ((isShiftPressed) && (clickedSlotUI.GetAssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)))
                {
                    _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                    clickedSlotUI.UpdateUISlot();
                    return;
                }
                else
                {
                    _mouseInventoryItem.UpdateMouseSlot(clickedSlotUI.GetAssignedInventorySlot);
                    clickedSlotUI.ClearSlot();
                    return;
                }
            }

            if ((clickedSlotUI.GetAssignedInventorySlot.GetItemData is null) &&
                (_mouseInventoryItem.AssingedInventorySlot.GetItemData is not null))
            {
                clickedSlotUI.GetAssignedInventorySlot.AssignItem(_mouseInventoryItem.AssingedInventorySlot);
                clickedSlotUI.UpdateUISlot();

                _mouseInventoryItem.ClearSlot();
                return;
            }

            if ((clickedSlotUI.GetAssignedInventorySlot.GetItemData is not null) &&
                (_mouseInventoryItem.AssingedInventorySlot.GetItemData is not null))
            {
                bool isSameItem = clickedSlotUI.GetAssignedInventorySlot.GetItemData ==
                                  _mouseInventoryItem.AssingedInventorySlot.GetItemData;

                if ((isSameItem) &&
                    (clickedSlotUI.GetAssignedInventorySlot.IsEnoughtRoomLeftInStack(_mouseInventoryItem
                        .AssingedInventorySlot.GetStackSize)))
                {
                    clickedSlotUI.GetAssignedInventorySlot.AssignItem(_mouseInventoryItem.AssingedInventorySlot);
                    clickedSlotUI.UpdateUISlot();

                    _mouseInventoryItem.ClearSlot();
                    return;
                }
                else if ((isSameItem) &&
                         (!clickedSlotUI.GetAssignedInventorySlot.TryToRoomLeftInStack(
                             _mouseInventoryItem.AssingedInventorySlot.GetStackSize, out int leftInStack)))
                {
                    if (leftInStack < 1)
                    {
                        SwapSlots(clickedSlotUI);
                    }
                    else
                    {
                        int remainingOnMouse = _mouseInventoryItem.AssingedInventorySlot.GetStackSize - leftInStack;
                        clickedSlotUI.GetAssignedInventorySlot.AddToStack(leftInStack);
                        clickedSlotUI.UpdateUISlot();

                        var newItem = new InventorySlot(_mouseInventoryItem.AssingedInventorySlot.GetItemData,
                            remainingOnMouse);
                        _mouseInventoryItem.ClearSlot();
                        _mouseInventoryItem.UpdateMouseSlot(newItem);
                        return;
                    }
                }
                else if (!isSameItem)
                {
                    SwapSlots(clickedSlotUI);
                    return;
                }
            }
        }

        private void SwapSlots(InventorySlotUI clickedSlotUI)
        {
            InventorySlot clonedSlot = new InventorySlot(_mouseInventoryItem.AssingedInventorySlot.GetItemData,
                _mouseInventoryItem.AssingedInventorySlot.GetStackSize);
            _mouseInventoryItem.ClearSlot();

            _mouseInventoryItem.UpdateMouseSlot(clickedSlotUI.GetAssignedInventorySlot);

            clickedSlotUI.ClearSlot();
            clickedSlotUI.GetAssignedInventorySlot.AssignItem(clonedSlot);
            clickedSlotUI.UpdateUISlot();
        }
    }
}