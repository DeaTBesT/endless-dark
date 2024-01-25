using System.Collections;
using Mirror;
using UnityEngine;

namespace Base
{
    public class EntityMove : NetworkBehaviour
    {
        [SerializeField] private float _defaultSpeed = 1f;

        #region [PublicVars]

        public float GetCurrentSpeed => _defaultSpeed;

        #endregion
        
        #region [PrivateVars]

        private float _speed;
        
        protected bool _isCanMove = true;
        private const float _moveTime = 1f;

        #endregion

        public void Initialize()
        {
            _speed = _defaultSpeed;
        }
        
        protected void MoveTo(Vector2Int input)
        {
            if (!_isCanMove)
            {
                return;
            }

            _isCanMove = false;
            StartCoroutine(MoveToRoutine(input));
        }

        private IEnumerator MoveToRoutine(Vector2Int input)
        {
            Vector2 toPosition = transform.position + (Vector3Int)input;
            
            for (float i = 0; i < 1; i += Time.deltaTime * _defaultSpeed)
            {
                transform.position = Vector2.LerpUnclamped(transform.position, toPosition, i);

                yield return null;
            }

            transform.position = new Vector2((int)toPosition.x, (int)toPosition.y);
            _isCanMove = true;
        }
    }
}