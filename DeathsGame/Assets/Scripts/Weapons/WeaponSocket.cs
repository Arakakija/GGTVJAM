using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSocket : MonoBehaviour
{
    [SerializeField] private CharacterController CController;
    public GameObject weapon;
    public BoxCollider2D collider2D = null;
    private Animator weaponAnimator;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public bool hasWeapon = false;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupWeapon(GameObject EquippedWeapon, bool isLeft = false)
    {
        if (EquippedWeapon == null) return;
        hasWeapon = true;
        weapon = EquippedWeapon;
        collider2D = weapon.GetComponent<BoxCollider2D>();
        weaponAnimator = transform.parent.GetComponent<Animator>();
        if (collider2D) collider2D.enabled = false;
        if (isLeft)
        {
            weapon.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }

    public bool isPlayingAnimation(string animationName)
    {
        return weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public void PlayAttackAnim(bool state)
    {
        weaponAnimator.SetBool(Attack,state);
    }
}
