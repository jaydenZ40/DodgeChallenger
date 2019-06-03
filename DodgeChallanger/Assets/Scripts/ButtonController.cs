using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public static ButtonController instance;

    public GameObject shopPanel;
    public GameObject localPlayer;

    private void Start()
    {
        instance = this;
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Intruction");
    }

    public void OnNext()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnShop()
    {
        shopPanel.SetActive(true);
    }

    public void OnCloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OnPan()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "Pan");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 10);
    }
}
