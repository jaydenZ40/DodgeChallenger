using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour
{
    public static Timer instance;
    public TextMeshProUGUI roundAndTimeTMP;
    public UnityEvent onRoundEnd = new UnityEvent();
    public UnityEvent onShoppingEnd = new UnityEvent();

    private float timer = 0;
    private bool isShoppingTime = false;
    private int roundNum = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
        timer += Time.deltaTime;

        if (!isShoppingTime)
        {
            roundAndTimeTMP.text = "Round " + roundNum + "\n   " + (30 - (int)timer) + " s";
            if (timer >= 30)
            {
                timer = 0;
                onRoundEnd.Invoke();
                isShoppingTime = true;
                roundNum++;
                roundAndTimeTMP.color = Color.green;
            }
        }

        if (isShoppingTime)
        {
            roundAndTimeTMP.text = "Shopping" + "\n    " + (15 - (int)timer) + " s";
            if (timer >= 15)
            {
                timer = 0;
                onShoppingEnd.Invoke();
                isShoppingTime = false;
                roundAndTimeTMP.color = Color.red;
            }
        }
    }

    public void ResetTimer()
    {
        timer = 0;
        isShoppingTime = false;
    }
}
