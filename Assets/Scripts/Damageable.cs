using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int damage;
    private float invincible;
    private IEnumerator coroutine;

    //If you collide with something that can damage you, start Invincibility, and when you leave that trigger, stop Invincibility
    
    private void Awake()
    {
        damage = 10;
        invincible = 1f;
    }

    private void Update()
    {
        coroutine = Invincibility();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "damage")
        {
            StartCoroutine(coroutine);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "damage")
        {
            StopAllCoroutines();
            Debug.Log("coroutine stopped");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "damage")
        {
            StartCoroutine(coroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "damage")
        {
            StopAllCoroutines();
            Debug.Log("coroutine stopped");
        }
    }

    private IEnumerator Invincibility()
    {
        for (;;)
        {
            Debug.Log("coroutine started");
            LevelManager.instance.TakeDamage(damage);
            yield return new WaitForSeconds(invincible);
        }
    }
}
