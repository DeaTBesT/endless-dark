using System;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class InventoyUIController : MonoBehaviour
    {
        [SerializeField] private DynamicInventoryDisplay _inventoryDisplay;

        private void Start()
        {
            _inventoryDisplay.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        }
        
        private void OnDisable()
        {
            InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        }

        private void Update()
        {
            if ((_inventoryDisplay.gameObject.activeInHierarchy) &&
                (Input.GetKeyDown(KeyCode.Escape)))
            {
                _inventoryDisplay.gameObject.SetActive(false);
            }
        }

        private void DisplayInventory(InventorySystem inventorySystem)
        {
            _inventoryDisplay.gameObject.SetActive(true);
            _inventoryDisplay.RefreshDynanicInventory(inventorySystem);
        }
    }
}