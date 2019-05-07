using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string bulletType = "P92";
    public float bulletSpeed = 3;

    private Vector3 bulletDirection;
    private int reflectTimeLeft = 4;

    private void Awake()
    {
        bulletDirection = GetMouseDirection();
    }

    private void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        transform.Translate(bulletDirection * Time.deltaTime * bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Boundary"))
        {
            bulletDirection = Vector2.Reflect(bulletDirection, other.contacts[0].normal);
            reflectTimeLeft--;
            if (reflectTimeLeft == 0)
            {
                Destroy(this.gameObject);
            }
        }

    }

    Vector3 GetMouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        return direction.normalized;
    }
}
