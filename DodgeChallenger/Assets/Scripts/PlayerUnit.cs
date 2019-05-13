using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    public Rigidbody2D rb;
    public GameObject Knife, Pan, P92, P1911, Shotgun, M416, Kar98k, AWM;
    public float moveSpeed = 3;
    public int bulletLeft = 7;
    public int maxBulletNum = 7;
    public bool isDodging = false;

    private GameObject currentWeapon;
    private bool isReloading = false;
    private bool hasGun = true;
    private bool isAttackReady = true;
    private float timer = 0;
    private float attackPrepareTime = 0.5f;
    private int damage = 1;
    [SerializeField]private float bulletSpeed = 3;
    private int health = 100;

    //enum Weapon { Knife, Pan, P92, P1911, Shotgun, M416, Kar98k, AWM };
    //Weapon currentWeapon = Weapon.Fist;

    private void Awake()
    {
        SetSpawnPosition();

        Timer.instance.ResetTimer();
        Timer.instance.onRoundEnd.AddListener(ExchangePosition);
        Timer.instance.onShoppingEnd.AddListener(ShootingRangeBorder);
    }

    private void FixedUpdate()
    {
        if (!hasAuthority)
            return;

        Move();
    }

    private void Update()
    {
        if (!hasAuthority)
            return;

        Attack();

        Reload();
    }

    void SetSpawnPosition()
    {
        if (FindObjectOfType<NetworkManager>().numPlayers % 2 == 0)
        {
            rb.transform.position = rb.transform.position = Vector3.zero;
            isDodging = true;
        }
        else
        {
            rb.transform.position = rb.transform.position = Vector3.left * 8;
            isDodging = false;
        }

        currentWeapon = P1911;
    }

    void Move()
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

    
    void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= GetAttackPrepareTime())
        {
            isAttackReady = true;
            timer = 0;
        }

        // shoot outside the battleground
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && bulletLeft > 0 && !isDodging && hasGun && isAttackReady)
        {
            CmdAttack1(rb.transform.position + GetMouseDirection() * 1.5f);
            bulletLeft--;
            LevelManager.instance.UpdateBulletNumber(bulletLeft, maxBulletNum);
            isAttackReady = false;
        }

        // use fist or knife, inside the battleground
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) /*&& !isDodging*/ && !hasGun && isAttackReady) // remove comment after testing
        {
            CmdAttack2(rb.transform.position + GetMouseDirection() / 1.5f);
            isAttackReady = false;
        }
    }

    [Command]
    void CmdAttack1(Vector3 pos)
    {
        GameObject go = Instantiate(currentWeapon, pos, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = (pos - rb.transform.position) * Time.deltaTime * bulletSpeed * 200;
        Destroy(go, 10f);
        NetworkServer.Spawn(go);
    }

    [Command]
    void CmdAttack2(Vector3 pos)
    {
        GameObject go = Instantiate(currentWeapon, pos, Quaternion.identity);
        Destroy(go, 0.25f);
        NetworkServer.Spawn(go);
    }

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
        LevelManager.instance.UpdateBulletNumber(bulletLeft, maxBulletNum);
    }

    Vector3 GetMouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = new Vector2(mousePosition.x - rb.transform.position.x, mousePosition.y - rb.transform.position.y);
        return direction.normalized;
    }

    void ExchangePosition()
    {
        if (!isDodging)
        {
            rb.transform.position = rb.transform.position = Vector3.zero;
            isDodging = true;
        }
        else
        {
            rb.transform.position = rb.transform.position = Vector3.left * 8;
            isDodging = false;
        }
    }

    void ShootingRangeBorder()
    {
        LevelManager.instance.SetShootingRangeBorder(hasGun);
    }

    public void GetShot()
    {
        if (isDodging)
        {
            health -= damage;
            LevelManager.instance.ShowTextChange(health);
            if (health <= 0)
            {
                string gameoverText = rb.name + " lose!";
                Debug.Log(gameoverText);
            }
        }       
    }
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (!isDodging)
    //    {
    //        return;
    //    }

    //    if (other.gameObject.CompareTag("Bullet"))
    //    {
    //        Destroy(other.gameObject);
    //        health -= damage;
    //        LevelManager.instance.ShowTextChange(health);
    //        if (health <= 0)
    //        {
    //            string gameoverText = rb.name + " lose!";
    //            Debug.Log(gameoverText);
    //        }
    //    }
    //}

    float GetAttackPrepareTime()
    {
        switch (currentWeapon.name)
        {
            case "Fist":
                damage = 1;
                return 0.5f;
            case "Knife":
                damage = 2;
                return 0.5f;
            case "P92":
                damage = 5;
                return 0.5f;
            case "P1911":
                damage = 5;
                return 0f;
            case "Shotgun":
                damage = 5;
                return 1.5f;
            case "M416":
                damage = 8;
                return 0f;
            case "Kar98k":
                damage = 25;
                return 3f;
            case "AWM":
                damage = 50;
                return 3f;
            default:
                damage = 1;
                return 0.5f;
        }
    }
}
