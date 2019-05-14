using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Rigidbody2D rb;

    private Vector3 bulletVelocity;
    private int damage = 1;

    private void Update()
    {
        bulletVelocity = rb.velocity;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Boundary"))
        {
            rb.velocity = Vector2.Reflect(bulletVelocity, other.contacts[0].normal);
        }

        if (other.transform.CompareTag("Player"))
        {
            if (LevelManager.instance.isP1Dodging && other.gameObject.name == "Player1")
            {
                LevelManager.instance.GetShot1(damage);
            }

            if (!LevelManager.instance.isP1Dodging && other.gameObject.name == "Player2")
            {
                LevelManager.instance.GetShot2(damage);
            }

            if (gameObject.CompareTag("Knife") || gameObject.CompareTag("Pan"))
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }

    int GetDamage()
    {
        switch (gameObject.name)
        {
            case "Fist":
                return 1;
            case "Knife":
                return 2;
            case "P92":
                return 5;
            case "P1911":
                return 5;
            case "Shotgun":
                return 5;
            case "M416":
                return 8;
            case "Kar98k":
                return 25;
            case "AWM":
                return 50;
            default:
                return 1;
        }
    }
}
