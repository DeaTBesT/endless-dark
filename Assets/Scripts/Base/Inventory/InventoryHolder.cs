using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryHolder : NetworkBehaviour
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] protected InventorySystem _primaryInventorySystem;

        public InventorySystem GetPrimaryInventorySystem => _primaryInventorySystem;

        public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;
        
        public override void OnStartClient()
        {
            Initialize();
        }
        
        public virtual void Initialize()
        {
            _primaryInventorySystem = new InventorySystem(_inventorySize);
        }
    }
}