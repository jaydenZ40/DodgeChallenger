using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Rigidbody2D rb;

    private Vector3 bulletVelocity;

    private void Update()
    {
        bulletVelocity = rb.velocity;
    }

    // Bullets' collision WILL NOT run in client, ONLY in the server.
    // Only reflect the bullets here. Other collisions run in PlayerUnit.cs
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Boundary"))
        {
            rb.velocity = Vector2.Reflect(bulletVelocity, other.contacts[0].normal);
        }
    }

    public static int GetDamage(string weapon)
    {
        switch (weapon)
        {
            case "Knife":
                return 1;
            case "Pan":
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
