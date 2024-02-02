using Interfaces;
using Mirror;
using UnityEngine;

namespace Interaction
{
    public abstract class Interactor : NetworkBehaviour
    {
        [SerializeField] protected LayerMask _interactingLayer;
        [SerializeField] protected float _interactingDistance = 2f;
        
        public bool IsInteracting { get; protected set; }

        protected virtual void StartInteraction(IInteractable interactable)
        {
            
        }
    }
}