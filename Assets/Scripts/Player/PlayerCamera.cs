using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private float _cameraSmothing;
        
        private Camera _camera;

        private PlayerMove _playerMove;
        
        public void Initialize(PlayerMove playerMove, Camera camera)
        {
            _camera = camera;
            
            if (!isLocalPlayer)
            {
                Destroy(_camera.gameObject);
                Destroy(this);
                
                return;
            }
        
            _playerMove = playerMove;
        }

        private void FixedUpdate()
        {
            if (_camera is null)
            {
                return;
            }
            
            Vector3 toPosition = new Vector3(transform.position.x, transform.position.y, -10);
            _camera.transform.position = Vector3.LerpUnclamped(_camera.transform.position, toPosition,
                _cameraSmothing * _playerMove.GetCurrentSpeed * Time.fixedDeltaTime);
        }
    }
}