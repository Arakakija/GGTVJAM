using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isChasing;
    public float pushForce;

    public FollowPlayer followPlayer;
    public float defaultFollowSpeed;

    public float delay;
    private void Start()
    {
        defaultFollowSpeed = followPlayer.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 dir = other.transform.position - transform.position;
            dir.Normalize();
            other.GetComponent<Health>().PlayerTakeDamage();
            other.GetComponent<Rigidbody2D>().AddForce(dir * pushForce);
            StartCoroutine(WaitForChase());

        }
    }


    IEnumerator WaitForChase()
    {
        followPlayer.moveSpeed = 0.0f;
        yield return new WaitForSeconds(delay);
        followPlayer.moveSpeed = defaultFollowSpeed;
        StopCoroutine(WaitForChase());
    }
}
