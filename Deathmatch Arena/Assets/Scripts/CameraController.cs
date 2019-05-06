using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Camera camera;

    private void Awake()
    {
        instance = this;
    }

    public void FollowPlayer(GameObject player)
    {
        Vector3 newPos = player.transform.position + new Vector3(0, 1.5f, -1.5f);    // a little higher than head
        camera.transform.SetPositionAndRotation(newPos, Quaternion.EulerRotation(0.35f, 0, 0));
        camera.transform.parent = player.transform;
        Debug.Log(camera.transform.position + ", player:" + player.transform.position);
    }


}
