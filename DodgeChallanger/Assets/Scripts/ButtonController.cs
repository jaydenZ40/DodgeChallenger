using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public static ButtonController instance;

    public GameObject shopPanel;
    public GameObject localPlayer;
    public GameObject KnifeIcon, PanIcon, GunIcon;

    private void Start()
    {
        instance = this;
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void OnNext()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnShop()
    {
        if (Timer.instance.isShoppingTime)
        {
            shopPanel.SetActive(true);
        }
    }

    public void OnCloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OnKnife()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "Knife");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 5);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 999;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 999;
        LocalManager.instance.UpdateBulletNumber(999, 999);
        GunIcon.SetActive(false);
        PanIcon.SetActive(false);
        KnifeIcon.SetActive(true);
    }

    public void OnPan()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "Pan");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 10);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 999;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 999;
        LocalManager.instance.UpdateBulletNumber(999, 999);
        GunIcon.SetActive(false);
        KnifeIcon.SetActive(false);
        PanIcon.SetActive(true);
    }

    public void OnP92()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "P92");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 15);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 7;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 7;
        LocalManager.instance.UpdateBulletNumber(7, 7);
        KnifeIcon.SetActive(false);
        PanIcon.SetActive(false);
        GunIcon.SetActive(true);
    }

    public void OnP1911()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "P1911");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 25);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 7;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 7;
        LocalManager.instance.UpdateBulletNumber(7, 7);
        KnifeIcon.SetActive(false);
        PanIcon.SetActive(false);
        GunIcon.SetActive(true);
    }

    public void OnS686()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "S686");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 35);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 7;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 7;
        LocalManager.instance.UpdateBulletNumber(7, 7);
        KnifeIcon.SetActive(false);
        PanIcon.SetActive(false);
        GunIcon.SetActive(true);
    }

    public void OnM416()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "M416");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 50);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 15;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 15;
        LocalManager.instance.UpdateBulletNumber(15, 15);
        KnifeIcon.SetActive(false);
        PanIcon.SetActive(false);
        GunIcon.SetActive(true);
    }

    public void OnKar98k()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "Kar98k");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 60);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 5;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 5;
        LocalManager.instance.UpdateBulletNumber(5, 5);
        KnifeIcon.SetActive(false);
        PanIcon.SetActive(false);
        GunIcon.SetActive(true);
    }

    public void OnAWM()
    {
        localPlayer.GetComponent<PlayerController>().SetCurrentWeapon(localPlayer.name, "AWM");
        localPlayer.GetComponent<PlayerController>().PurchaseItem(localPlayer.name, 99);
        localPlayer.GetComponent<PlayerController>().maxBulletNum = 3;
        localPlayer.GetComponent<PlayerController>().bulletLeft = 3;
        LocalManager.instance.UpdateBulletNumber(3, 3);
        KnifeIcon.SetActive(false);
        PanIcon.SetActive(false);
        GunIcon.SetActive(true);
    }

    public GameObject GetLocalPlayer()
    {
        return localPlayer;
    }
}