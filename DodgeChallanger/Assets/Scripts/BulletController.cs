using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{
    private Rigidbody2D rb;
    private Vector3 bulletVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity * Time.deltaTime * 1;
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
            if (GameObject.Find(_playerID).GetComponent<PlayerController>().isDodging)
            {
                CmdShotAt(_playerID);
            }
            if (gameObject.CompareTag("MeleeWeapon"))
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }

    [Command]
    void CmdShotAt(string _playerID)
    {
        GameObject.Find(_playerID).GetComponent<PlayerController>().TakeDamage(_playerID);

    }
}
