using System;
using Inventory;
using Player;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private InventoryItemData _itemData;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InventoryHolder playerInventory))
        {
            if (playerInventory.GetInventorySystem.AddToInventory(_itemData, 1))
            {
                Destroy(gameObject);
            }
        }
    }
}
