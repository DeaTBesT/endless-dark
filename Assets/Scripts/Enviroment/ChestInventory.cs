using Interaction;
using Interfaces;
using Inventory;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractableComplete { get; set; }
    
    public void Interact(Interactor interactor, out bool inSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(_inventorySystem);
        inSuccessful = true;
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }
}
