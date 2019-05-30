using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Player_Health : NetworkBehaviour
{
    [SyncVar (hook = "OnHealthChanged")] private int health = 100;
    private TextMeshProUGUI healthTMP;

    private void Start()
    {
        healthTMP = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        SetHealthText();
    }

    void SetHealthText()
    {
        if (isLocalPlayer)
        {
            healthTMP.text = "Health: " + health;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    void OnHealthChanged(int h)
    {
        health = h;
        SetHealthText();
    }
}
