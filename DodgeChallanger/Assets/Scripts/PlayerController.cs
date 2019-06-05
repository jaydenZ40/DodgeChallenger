using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject Knife, Pan, P92, P1911, S686, M416, Kar98k, AWM;
    public GameObject currentWeapon;
    public float moveSpeed = 3;
    public int bulletLeft = 7;
    public int maxBulletNum = 7;
    public float bulletSpeed = 3;
    [SyncVar] public bool isDodging = false;
    public int damage = 1;

    private Rigidbody2D rb;
    private bool isReloading = false;
    private bool isShopping = false;
    private bool isAttackReady = true;
    private float timer = 0;
    private bool runOnce = true;

    void Start()
    {
        Timer.instance.ResetTimer();

        if (!isLocalPlayer)
        {
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        rb.GetComponent<SpriteRenderer>().color = Color.red;
        ButtonController.instance.localPlayer = rb.gameObject;
        Timer.instance.onRoundEnd.AddListener(ShoppingPause);
        Timer.instance.onShoppingEnd.AddListener(ExchangePosition);
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (runOnce)
        {
            runOnce = false;
            SetSpawnPosition();
        }

        Move();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Attack();

        Reload();
    }

    void SetSpawnPosition()
    {
        if (FindObjectOfType<NetworkManager>().numPlayers % 2 == 0)
        {
            rb.transform.position = rb.transform.position = Vector3.zero;
            CmdSetDodging(rb.name, true);
        }
        else
        {
            rb.transform.position = rb.transform.position = Vector3.left * 6f;
            CmdSetDodging(rb.name ,false);
        }

        currentWeapon = Knife; // change to knife after testing
    }

    [Command]
    void CmdSetDodging(string ID, bool b)
    {
        GameObject.Find(ID).GetComponent<PlayerController>().isDodging = b;
    }

    void Move()
    {
        if (!isShopping)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
            rb.velocity = Vector3.zero; // ignore the effect of collision with weapons
        }
    }

    void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= GetAttackPrepareTime())
        {
            isAttackReady = true;
            timer = 0;
        }

        // shoot outside the battleground
        if (Input.GetKeyDown(KeyCode.Mouse0) && bulletLeft > 0 && !isShopping && !isDodging && isAttackReady)
        {
            CmdAttack(rb.transform.position + GetMouseDirection() * 1.5f, rb.position);
            bulletLeft--;
            LocalManager.instance.UpdateBulletNumber(bulletLeft, maxBulletNum);
            isAttackReady = false;
        }

        //// use fist or knife, inside the battleground
        //if (Input.GetKeyDown(KeyCode.Mouse0) && !isShopping && !isDodging && !hasGun && isAttackReady)
        //{
        //    CmdAttack2(rb.transform.position + GetMouseDirection() * 1.25f);
        //    isAttackReady = false;
        //}
    }

    [Command]
    void CmdAttack(Vector3 spawnPos, Vector3 playerPos)    //  shoot bullets
    {
        GameObject go = Instantiate(currentWeapon, spawnPos, Quaternion.identity);
        if (go.CompareTag("MeleeWeapon"))
        {
            go.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Destroy(go, 0.5f);
        }
        else
        {
            go.GetComponent<Rigidbody2D>().velocity = (spawnPos - playerPos) * Time.deltaTime * bulletSpeed * 200;
            Destroy(go, 10f);
        }
        NetworkServer.Spawn(go);
    }

    //[Command]
    //void CmdAttack2(Vector3 pos)    // knife or pan
    //{
    //    GameObject go = Instantiate(currentWeapon, pos, Quaternion.identity);
    //    Destroy(go, 0.5f);
    //    NetworkServer.Spawn(go);
    //}

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            Invoke("DelayReloadTime", 2);
            isReloading = true;
        }
    }

    void DelayReloadTime()
    {
        bulletLeft = maxBulletNum;
        isReloading = false;
        LocalManager.instance.UpdateBulletNumber(bulletLeft, maxBulletNum);
    }

    Vector3 GetMouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = new Vector2(mousePosition.x - rb.transform.position.x, mousePosition.y - rb.transform.position.y);
        return direction.normalized;
    }

    void ShoppingPause()
    {
        isShopping = true;
    }

    void ExchangePosition()
    {
        if (!isDodging)
        {
            rb.gameObject.SetActive(false);
            rb.transform.position = rb.transform.position = Vector3.zero;
            isDodging = true;
            rb.gameObject.SetActive(true);
        }
        else
        {
            rb.gameObject.SetActive(false);
            rb.transform.position = rb.transform.position = Vector3.left * 8.3f;
            isDodging = false;
            rb.gameObject.SetActive(true);
        }
        isShopping = false;
    }

    float GetAttackPrepareTime()
    {
        switch (currentWeapon.name)
        {
            case "Knife":
                return 1f;
            case "Pan":
                return 2f;
            case "P92":
                return 0.5f;
            case "P1911":
                return 0f;
            case "S686":
                return 1.5f;
            case "M416":
                return 0f;
            case "Kar98k":
                return 3f;
            case "AWM":
                return 3f;
            default:
                return 0.5f;
        }
    }

    public void TakeDamage(string ID)
    {
        CmdTellServerWhoWasShot(ID);
    }

    [Command]
    void CmdTellServerWhoWasShot(string ID)
    {
        Debug.Log(ID + " has been shot.");
        GameObject go = GameObject.Find(ID);
        GameObject shooter = FindShooter(ID);
        go.GetComponent<Player_Health>().TakeDamage(shooter.GetComponent<Player_Health>().damage);
        shooter.GetComponent<Player_Health>().GetGold(shooter.GetComponent<Player_Health>().damage);
    }

    GameObject FindShooter(string ID)
    {
        GameObject go1 = GameObject.FindGameObjectsWithTag("Player")[0];
        GameObject go2 = GameObject.FindGameObjectsWithTag("Player")[1];
        return go1.name == ID ? go2 : go1;
    }

    public void PurchaseItem(string ID, int g)
    {
        CmdPurchaseItem(ID, g);
    }

    [Command]
    void CmdPurchaseItem(string ID, int g)
    {
        GameObject go = GameObject.Find(ID);
        go.GetComponent<Player_Health>().TakeGold(g);
    }

    public void SetCurrentWeapon(string ID, string weapon)
    {
        CmdSetCurrentWeapon(ID, weapon);
    }

    [Command]
    public void CmdSetCurrentWeapon(string ID, string weapon)
    {
        GameObject go = GameObject.Find(ID);
        switch (weapon)
        {
            case "Knife":
                currentWeapon = Knife;
                go.GetComponent<Player_Health>().damage = 1;
                break;
            case "Pan":
                currentWeapon = Pan;
                go.GetComponent<Player_Health>().damage = 2;
                break;
            case "P92":
                currentWeapon = P92;
                go.GetComponent<Player_Health>().damage = 5;
                break;
            case "P1911":
                currentWeapon = P1911;
                go.GetComponent<Player_Health>().damage = 5;
                break;
            case "S686":
                currentWeapon = S686;
                go.GetComponent<Player_Health>().damage = 10;
                break;
            case "M416":
                currentWeapon = M416;
                go.GetComponent<Player_Health>().damage = 10;
                break;
            case "Kar98k":
                currentWeapon = Kar98k;
                go.GetComponent<Player_Health>().damage = 25;
                break;
            case "AWM":
                currentWeapon = AWM;
                go.GetComponent<Player_Health>().damage = 50;
                break;
            default:
                currentWeapon = Knife;
                go.GetComponent<Player_Health>().damage = 1;
                break;
        }
    }
}