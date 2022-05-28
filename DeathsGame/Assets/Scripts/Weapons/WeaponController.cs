using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Stats;
using UnityEngine;
using Weapons;
using Weapons.Stats;

public class WeaponController : MonoBehaviour
{
    public GameObject instigator = null;
    [SerializeField] public bool isDropped = true;
    [SerializeField] private Weapon weapon;

    private WeaponSocket sockets;
    
    private void Start()
    {
        instigator = transform.root.gameObject;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || isDropped || other.gameObject.GetComponent<WeaponController>()) return;


        if (other.gameObject.transform.root == instigator.transform) return;
        
        other.GetComponent<Health>().TakeDamage(instigator,instigator.GetComponent<BaseStats>().GetStat(Stat.Attack));
        Vector2 dir = other.transform.position - instigator.transform.position;
        float force = instigator.GetComponent<BaseStats>().GetStat(Stat.Attack) * weapon.GetStat(WeaponStats.PushForce);

        other.GetComponent<Rigidbody2D>().AddForce(dir * force);
    }
}
