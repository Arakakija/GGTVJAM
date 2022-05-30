using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public GameObject instigator = null;
    
    void Start()
    {
        instigator = transform.root.gameObject;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.root == instigator.transform || other.gameObject.layer == 6) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(instigator,instigator.GetComponent<BaseStats>().GetStat(Stat.Attack));
            Vector2 dir = other.transform.position - instigator.transform.position;
            float force = instigator.GetComponent<BaseStats>().GetStat(Stat.Attack);

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * force * 100);
        }
    }
}
