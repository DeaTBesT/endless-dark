using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : Base.Input
    {
        public Action<Vector2Int> OnMove { get; set; }

        private const string _horizontalInput = "Horizontal";
        private const string _verticalInput = "Vertical";

        public override void HandlerUpdate()
        {
            OnMove?.Invoke(new Vector2Int((int)Input.GetAxisRaw(_horizontalInput), 
                (int)Input.GetAxisRaw(_verticalInput)));
        }

        public override void HandlerFixedUpdate()
        {
            
        }
    }
}