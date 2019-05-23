using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public TextMeshProUGUI bulletNumberTMP;
    public TextMeshProUGUI healthTMP1, goldTMP1, healthTMP2, goldTMP2;
    public Collider2D ShootingRangeBorder;
    public bool isP1Dodging = false;
    public int damage = 1;

    private int health1 = 100;
    private int health2 = 100;
    private int gold1 = 0;
    private int gold2 = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Timer.instance.onRoundEnd.AddListener(GoldConvert);
        Timer.instance.onShoppingEnd.AddListener(HealthConvert);
    }

    void Update()
    {
        healthTMP1.text = "Health: " + health1;
        goldTMP1.text = "Gold: " + gold1;

        healthTMP2.text = "Health: " + health2;
        goldTMP2.text = "Gold: " + gold2;
    }

    void GoldConvert()
    {
        int temp = health1; health1 = 100; gold1 = temp;

        temp = health2; health2 = 100; gold2 = temp;

        isP1Dodging = !isP1Dodging;
    }

    void HealthConvert()
    {
        int temp = gold1; gold1 = 0; health1 += temp / 10;

        temp = gold2; gold2 = 0; health2 += temp / 10;
    }

    public void SetHealth(int h1, int h2)
    {
        health1 = h1; health2 = h2;
    }

    public int GetHealth1()
    {
        return health1;
    }

    public int GetHealth2()
    {
        return health2;
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
