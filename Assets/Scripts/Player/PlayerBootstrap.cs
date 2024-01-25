using Base;
using UnityEngine;

namespace Player
{
    public class PlayerBootstrap : Bootstrap
    {
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private PlayerInteract _playerInteract;
        
        [SerializeField] private Camera _camera;
        
        private PlayerInput _input;
        
        public override void OnStartClient()
        {
            _input = new PlayerInput();
            
            _playerMove.Initialize(_input);
            _playerCamera.Initialize(_playerMove, _camera);
            _playerInteract.Initialize(_camera);
        }

        public override void OnStopClient()
        {
            _playerMove.Deinitialize();
        }
    }
}