using Base;
using UnityEngine;

namespace Player
{
    public class PlayerMove : EntityMove
    {
        private PlayerInput _input;

        public void Initialize(PlayerInput input)
        {
            Initialize();
  
            _input = input;

            _input.OnMove += OnMove;
        }

        public void Deinitialize()
        {
            _input.OnMove -= OnMove;
        }
    
        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            
            _input.HandlerUpdate();
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }
        }

        private void OnMove(Vector2Int input)
        {
            MoveTo(input);
        }
    }
}