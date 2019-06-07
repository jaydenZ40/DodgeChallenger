using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Player_Health : NetworkBehaviour
{
    [SyncVar] public int damage = 1;

    [SyncVar (hook = "OnHealthChanged")] private int health = 100;
    [SyncVar(hook = "OnGoldChanged")] private int gold = 0;
    private TextMeshProUGUI healthTMP, goldTMP;

    private void Start()
    {
        healthTMP = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        goldTMP = GameObject.Find("Gold").GetComponent<TextMeshProUGUI>();
        SetHealthText();
    }

    void SetHealthText()
    {
        if (isLocalPlayer)
        {
            healthTMP.text = "Health: " + health;
        }
    }
    void SetGoldText()
    {
        if (isLocalPlayer)
        {
            goldTMP.text = "Gold: " + gold;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    public void TakeGold(int g) // allow borrow money by health.
    {
        if (gold >= g)
        {
            gold -= g;
        }
        else
        {
            health -= (g - gold);
        }
    }

    public void GetGold(int g)
    {
        gold += g;
    }

    void OnHealthChanged(int h)
    {
        health = h;
        SetHealthText();
    }

    void OnGoldChanged(int g)
    {
        gold = g;
        SetGoldText();
    }

    public int GetHealth()
    {
        return health;
    }
}
