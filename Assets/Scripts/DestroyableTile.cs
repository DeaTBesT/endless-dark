using Mirror;

public class DestroyableTile : NetworkBehaviour
{
    [Client]
    public void DestroyTile()
    {
        DestroyTileCmd();
    }

    [Command(requiresAuthority = false)]
    private void DestroyTileCmd()
    {
        NetworkServer.Destroy(gameObject);
    }
}
