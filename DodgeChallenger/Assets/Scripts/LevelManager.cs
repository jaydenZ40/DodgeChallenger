using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager instance;
    public TextMeshProUGUI bulletNumberTMP;
    public TextMeshProUGUI healthTMP, goldTMP;
    public Collider2D ShootingRangeBorder;

    private int health = 100;
    private int gold = 0;
    private int damage = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Timer.instance.onRoundEnd.AddListener(GoldConvert);
        Timer.instance.onShoppingEnd.AddListener(HealthConvert);
    }

    void GoldConvert()
    {
        int temp = health; health = 100; gold = temp;
        ShowTextChange(health);
    }

    void HealthConvert()
    {
        int temp = gold; gold = 0; health += gold;
        ShowTextChange(health);
    }

    public void ShowTextChange(int h)
    {
        health = h;
        healthTMP.text = "Health: " + health;
        goldTMP.text = "Gold: " + gold;
    }

    public void UpdateBulletNumber(int bulletLeft, int maxBullet)
    {
        bulletNumberTMP.text = (bulletLeft + " / " + maxBullet);
    }

    public bool SetShootingRangeBorder(bool hasGun)
    {
        if (hasGun)
        {
            ShootingRangeBorder.gameObject.SetActive(false);
            return false;
        }
        else
        {
            ShootingRangeBorder.gameObject.SetActive(true);
            return true;
        }
    }
}
