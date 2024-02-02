using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryHolder : NetworkBehaviour
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] protected InventorySystem _inventorySystem;

        public InventorySystem GetInventorySystem => _inventorySystem;

        public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;
        
        public void Initialize()
        {
            _inventorySystem = new InventorySystem(_inventorySize);
        }
    }
}