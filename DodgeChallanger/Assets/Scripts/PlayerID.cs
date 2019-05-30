using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour
{
    [SyncVar] public string playerUniqueID;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        GetNetID();
        SetID();
    }

    private void Awake()
    {
        myTransform = transform;
    }

    private void Update()
    {
        if (myTransform.name == "" || myTransform.name == "Player(Clone)")
        {
            SetID();
        }
    }

    [Client]
    void GetNetID()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyID(MakeUniqueID());
    }

    void SetID()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueID;
        }
        else
        {
            myTransform.name = MakeUniqueID();
        }
    }

    string MakeUniqueID()
    {
        string uniqueID = "Player" + playerNetID.ToString();
        return uniqueID;
    }

    [Command]
    void CmdTellServerMyID(string name)
    {
        playerUniqueID = name;
    }
}
