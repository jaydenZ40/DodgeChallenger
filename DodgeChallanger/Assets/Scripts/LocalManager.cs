using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalManager : MonoBehaviour
{
    public static LocalManager instance;

    public TextMeshProUGUI bulletNumberTMP;
    public GameObject GameOverPanel;
    public TextMeshProUGUI GameOverTMP;
    public int gold;
    public string weapon;

    private void Awake()
    {
        instance = this;

        //Physics2D.IgnoreLayerCollision(8, 8);
    }

    public void UpdateBulletNumber(int bulletLeft, int maxBullet)
    {
        bulletNumberTMP.text = (bulletLeft + " / " + maxBullet);
    }

    public void Lose()
    {
        GameOverTMP.text = "You Lose!";
        GameOverPanel.SetActive(true);
        ButtonController.instance.GetLocalPlayer().GetComponent<PlayerController>().ShutDownNetwork();
        Destroy(GameObject.Find("NetworkManager"));
    }
}
