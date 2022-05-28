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
    
    [SerializeField] private Transform rightHandTransform; 
    [SerializeField] private Weapon defaultWeapon = null;
    
    private Weapon currentWeapon;
    void Start()
    {
        EquipWeapon(defaultWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack(true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Attack(false);
        }
    }
    
    public bool hasWeapon()
    {
        return currentWeapon != null;
    }
    
    public void EquipWeapon(Weapon weapon)
    {
        if(weapon == null || hasWeapon()) return;
        
        if (currentWeapon == null)
            currentWeapon = weapon;

        weapon.Spawn(rightHandTransform);
    }
    
    void Attack(bool state)
    {
        if(!hasWeapon()) return;
        WeaponSocket weaponSocket = rightHandTransform.GetComponent<WeaponSocket>();

        if (weaponSocket == null || weaponSocket.weapon == null || weaponSocket.isPlayingAnimation("Attack")) return;
        weaponSocket.collider2D.enabled = state;
        weaponSocket.PlayAttackAnim(state);
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


