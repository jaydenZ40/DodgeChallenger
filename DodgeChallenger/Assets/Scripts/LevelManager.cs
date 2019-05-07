using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager instance;
    public TextMeshProUGUI bulletNumberTMP;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateBulletNumber(int bulletLeft, int maxBullet)
    {
        bulletNumberTMP.text = (bulletLeft + " / " + maxBullet);
    }
}
