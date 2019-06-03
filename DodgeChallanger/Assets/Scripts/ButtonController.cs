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

    public void OnKnife()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "Knife");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 5);
    }

    public void OnPan()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "Pan");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 10);
    }

    public void OnP92()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "P92");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 15);
    }

    public void OnP1911()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "P1911");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 25);
    }

    public void OnS686()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "S686");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 35);
    }

    public void OnM416()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "M416");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 50);
    }

    public void OnKar98k()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "Kar98k");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 60);
    }

    public void OnAWM()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "AWM");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 99);
    }
}