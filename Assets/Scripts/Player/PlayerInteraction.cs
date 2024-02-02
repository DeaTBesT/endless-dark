using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerInteraction : Base.EntityInteraction
    {
        private Camera _camera;

        public void Initialize(Camera camera)
        {
            _camera = camera;
        }

        private void Update()
        {
            if ((!isLocalPlayer) || (_camera is null))
            {
                return;
            }

            //Rewrite input
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePoint = _camera.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero, Mathf.Infinity,
                    _interactingLayer);

                if (hit.transform is null)
                {
                    return;
                }

                if (hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    StartInteraction(interactable);
                }
            }
        }

        protected override void StartInteraction(IInteractable interactable)
        {
            interactable.Interact(this, out bool isSuccessful);
            IsInteracting = isSuccessful;
        }

        private void EndInteraction()
        {
            IsInteracting = false;
        }
    }
}