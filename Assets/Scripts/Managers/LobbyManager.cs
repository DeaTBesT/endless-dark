using Mirror;
using UnityEngine;

namespace Managers
{
    public class LobbyManager : MonoBehaviour
    {
        [Header("Scenes")]
        [Scene]
        [SerializeField] private string _onlineScene;
        [Scene]
        [SerializeField] private string _debugScene;

        [Header("Debug")]
        [SerializeField] private bool _isDebug;

        [Header("Components")]
        [SerializeField] private NetworkManager _networkManager;

        private void Start()
        {
            _networkManager.onlineScene = _isDebug ? _debugScene : _onlineScene;
        }
    }
}