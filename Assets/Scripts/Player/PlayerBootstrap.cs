using Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerBootstrap : Bootstrap
    {
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private PlayerInteraction playerInteraction;
        [FormerlySerializedAs("_playerInventory")] [SerializeField] private PlayerInventoryHolder playerInventoryHolder;
        
        [Space]
        [SerializeField] private Camera _camera;
        
        private PlayerInput _input;
        
        public override void OnStartClient()
        {
            _input = new PlayerInput();
            
            _playerMove.Initialize(_input);
            _playerCamera.Initialize(_playerMove, _camera);
            playerInteraction.Initialize(_camera);
            playerInventoryHolder.Initialize();
        }

        public override void OnStopClient()
        {
            _playerMove.Deinitialize();
        }
    }
}