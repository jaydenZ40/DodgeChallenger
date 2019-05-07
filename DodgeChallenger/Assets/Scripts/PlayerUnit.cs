using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    public static PlayerUnit instance;
    public GameObject bullet;
    public float moveSpeed = 3;
    public float damage = 1;
    public int bulletLeft = 7;
    public int maxBulletNum = 7;
    public UnityEvent onShooting = new UnityEvent();
    public UnityEvent onReloading = new UnityEvent();

    private Rigidbody2D rb;
    private bool isReloading = false;

    enum Weapon { Fist, Knife, Pan, P92, P1911, Shotgun, M416, Kar98k, AWM };
    Weapon currentWeapon = Weapon.Fist;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
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

        CmdShoot();

        Reload();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }

    [Command]
    void CmdShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && bulletLeft > 0)
        {
            GameObject go = Instantiate(bullet, rb.transform.position + GetMouseDirection() * 1.5f, Quaternion.identity);
            bulletLeft--;
            LevelManager.instance.UpdateBulletNumber(bulletLeft, maxBulletNum);
            NetworkServer.Spawn(go);
        }
    }

    void Reload()
    {
        if (!hasAuthority)
            return;

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
}
