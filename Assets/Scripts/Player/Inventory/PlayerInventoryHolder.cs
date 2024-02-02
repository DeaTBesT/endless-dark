using Base;
using Inventory;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Player
{
    public class PlayerInventoryHolder : EntityInventory
    {
        [SerializeField] protected int _secondaryInventorySize;
        [SerializeField] protected InventorySystem _secondaryInventorySystem;

        public InventorySystem GetSecondaryInventorySystem => _secondaryInventorySystem;
        
        public override void Initialize()
        {
            base.Initialize();

            _secondaryInventorySystem = new InventorySystem(_secondaryInventorySize);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OnDynamicInventoryDisplayRequested?.Invoke(_secondaryInventorySystem);
            }
        }

        public bool AddToInventory(InventoryItemData itemData, int amount)
        {
            if (_primaryInventorySystem.AddToInventory(itemData, amount))
            {
                return true;
            }
            else if (_secondaryInventorySystem.AddToInventory(itemData, amount))
            {
                return true;
            }

            return false;
        }
    }
}