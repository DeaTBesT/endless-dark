using UnityEngine;

public class PlayerInteract : EntityInteraction
{
    [SerializeField] private LayerMask _layerMask;

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

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint = _camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero, Mathf.Infinity,
                _layerMask);

            if (hit.transform is null)
            {
                return;
            }

            if (hit.transform.TryGetComponent(out DestroyableTile tile))
            {
                tile.DestroyTile();
            }
        }
    }
}