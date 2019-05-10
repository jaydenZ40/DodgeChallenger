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

    private int health = 100;
    private int gold = 0;

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
        ShowTextChange();
    }

    void HealthConvert()
    {
        int temp = gold; gold = 0; health += gold;
        ShowTextChange();
    }

    void ShowTextChange()
    {
        healthTMP.text = "Health: " + health;
        goldTMP.text = "Gold: " + gold;
    }

    public void UpdateBulletNumber(int bulletLeft, int maxBullet)
    {
        bulletNumberTMP.text = (bulletLeft + " / " + maxBullet);
    }
}
