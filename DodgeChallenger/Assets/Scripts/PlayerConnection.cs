using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour
{
    public GameObject PlayerUnitPrefab;

    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Debug.Log("PlayerConnection::Start -- Spawning my own personal unit...");
        CmdSpawnMyUnit();
    }

    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(PlayerUnitPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

        int playerNum = FindObjectOfType<NetworkManager>().numPlayers;
        go.name = "Player" + playerNum.ToString();
        //if (playerNum % 2 == 0)
        //{
        //    go.layer = LayerMask.NameToLayer("RemotePlayer");
        //    go.GetComponent<SpriteRenderer>().color = Color.red;
        //}
    }
}