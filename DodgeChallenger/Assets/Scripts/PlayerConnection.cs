﻿using System.Collections;
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
        GameObject go = Instantiate(PlayerUnitPrefab);
        myPlayerUnit = go;
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}