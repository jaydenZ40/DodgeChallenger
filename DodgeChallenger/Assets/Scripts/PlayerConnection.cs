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

    void Update()
    {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    CmdSpawnMyUnit();
        //}
    }

    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(PlayerUnitPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

        go.name = "Player" + FindObjectOfType<NetworkManager>().numPlayers.ToString();
    }
}