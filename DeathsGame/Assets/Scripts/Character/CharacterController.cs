using System;
using System.Collections;
using System.Collections.Generic;
using Saving;
using Stats;
using UnityEngine;
using Weapons;

public class CharacterController : MonoBehaviour,ISaveable,IModifierProvider
{
    public BaseStats baseStats;
    
    public Animator anim;
    
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    private float nextFire = 0.0f;

    public GameObject rayProjectile;
    public Transform firePosition;
    public Transform firePoint;

    private bool isAttacking;

    public Rigidbody2D rb;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        FireTransformPosition();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            AttackMelee(true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isAttacking = false;
            AttackMelee(false);
            anim.speed = 1;
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
        }
        
        if (Input.GetButton("Fire2"))
        {
            Fire();
        }
        
        if (Input.GetButtonUp("Fire2"))
        {
            isAttacking = false;
        }  
    }
    
    void AttackMelee(bool state)
    {
        anim.speed = baseStats.GetStat(Stat.AttackSpeed);
        anim.SetBool(Attack1,state);
    }
    
    void Fire()
    {
        if (Time.time  > nextFire)
        {
            nextFire = Time.time + baseStats.GetStat(Stat.AttackSpeed);
            GameObject go = Instantiate(rayProjectile, firePoint.transform.position, firePoint.rotation);
            go.transform.parent = this.gameObject.transform;
            Projectile bullet = go.GetComponent<Projectile>();
            if (bullet == null) return;
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bullet.speed);
        }
    }
    
    void FireTransformPosition()
    {
        if(rb.velocity.x > 0 && rb.velocity.y == 0)
            firePosition.rotation = Quaternion.Euler(0,0,0);
        else if(rb.velocity.y > 0)
            firePosition.rotation = Quaternion.Euler(0,0,90);
        else if(rb.velocity.x < 0 &&  rb.velocity.y == 0)
            firePosition.rotation = Quaternion.Euler(0,0,180);
        else if(rb.velocity.y < 0)
            firePosition.rotation = Quaternion.Euler(0,0,-90);
    }
    
    public IEnumerator<float> GetAdditiveModifier(Stat stat)
    {
        if (stat == Stat.Attack)
        {
            yield return 0;
        }
    }
    
    public object CaptureState()
    {
        throw new NotImplementedException();
    }

    public void RestoreState(object state)
    {
        throw new NotImplementedException();
    }
}


