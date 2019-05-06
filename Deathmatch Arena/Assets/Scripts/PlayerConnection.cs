using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour
{
    public GameObject PlayerUnitPrefab;

    void Start()
    {
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

    GameObject myPlayerUnit;

    // Command are special functions that ONLY get executed on the server.
    [Command]
    void CmdSpawnMyUnit()
    {
        int n = ++LevelManager.playerNum;
        GameObject go = n == 1 ? Instantiate(PlayerUnitPrefab, Vector3.zero, Quaternion.identity)
            : Instantiate(PlayerUnitPrefab, Vector3.right * 7, Quaternion.identity);
        myPlayerUnit = go;
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        CameraController.instance.FollowPlayer(myPlayerUnit);
        myPlayerUnit.name = "Player" + LevelManager.playerNum.ToString();
    }
}
