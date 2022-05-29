using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using Unity.VisualScripting;
using UnityEngine;
using Weapons.Stats;

public class Projectile : MonoBehaviour
{
    public GameObject instigator = null;

    public float speed = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        instigator = transform.root.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.root == instigator.transform) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(instigator,instigator.GetComponent<BaseStats>().GetStat(Stat.Attack));
            Vector2 dir = other.transform.position - instigator.transform.position;
            float force = instigator.GetComponent<BaseStats>().GetStat(Stat.Attack);

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * force);
        }
        Destroy(gameObject);
    }
}
