using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 bulletVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bulletVelocity = rb.velocity;
    }

    // Bullets' collision WILL NOT run in client, ONLY in the server.
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Boundary"))
        {
            rb.velocity = Vector2.Reflect(bulletVelocity, other.contacts[0].normal);
        }

        if (other.transform.CompareTag("Player"))
        {
            string _playerID = other.gameObject.name;
            if (_playerID == LocalManager.instance.dodgingID)
            {
                GameObject.Find(_playerID).GetComponent<PlayerController>().TakeDamage(_playerID);
            }
            if (gameObject.CompareTag("MeleeWeapon"))
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }
}
