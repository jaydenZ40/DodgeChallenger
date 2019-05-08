﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;

    private Vector3 bulletVelocity;
    //    private int reflectTimeLeft = 4;

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

    }
}
