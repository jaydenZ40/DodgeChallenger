using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalManager : MonoBehaviour
{

    public static LocalManager instance;

    public TextMeshProUGUI bulletNumberTMP;
    public int gold;
    public string weapon;
    public string dodgingID;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateBulletNumber(int bulletLeft, int maxBullet)
    {
        bulletNumberTMP.text = (bulletLeft + " / " + maxBullet);
    }
}
