using Interaction;
using UnityEngine.Events;

namespace Interfaces
{
    public interface IInteractable
    {   
        public UnityAction<IInteractable> OnInteractableComplete { get; set; }
        
        public void Interact(Interactor interactor, out bool inSuccessful);
        public void EndInteraction();
    }
}
