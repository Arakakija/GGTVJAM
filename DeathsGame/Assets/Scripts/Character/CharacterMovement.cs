using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private Rigidbody2D rb;
    
    private Vector2 direction;
    
    private static readonly int AnimX = Animator.StringToHash("AnimX");
    private static readonly int AnimY = Animator.StringToHash("AnimY");

    private Vector2 lastMoveDirection;
    private Vector2 attackDirection;
    private static readonly int AnimMagnitude = Animator.StringToHash("AnimMagnitude");
    private static readonly int AnimLastMoveX = Animator.StringToHash("AnimLastMoveX");
    private static readonly int AnimLastMoveY = Animator.StringToHash("AnimLastMoveY");
    private static readonly int AnimaAttackX = Animator.StringToHash("AnimaAttackX");
    private static readonly int AnimaAttackY = Animator.StringToHash("AnimaAttackY");

    private void Start()
    {
    }

    private void Update()
    {
        moveInput();   
        Animate();
    }


    private void FixedUpdate()
    {
        Move();
    }

    void moveInput()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        if ( direction.x != 0 || direction.y != 0)
        {
            attackDirection = direction;
        }
        if ((hInput == 0 && vInput == 0) && direction.x != 0 || direction.y != 0)
        {
            lastMoveDirection = direction;
            attackDirection = direction;
        }
 
        
        direction = new Vector2(hInput, vInput).normalized;
    }
    
    void Move()
    {
        rb.velocity = new Vector2(direction.x * _characterController.baseStats.GetStat(Stat.MoveSpeed), direction.y * _characterController.baseStats.GetStat(Stat.MoveSpeed));
    }

    void Animate()
    {
        _characterController.anim.SetFloat(AnimX,direction.x);
        _characterController.anim.SetFloat(AnimY,direction.y);        
        _characterController.anim.SetFloat(AnimMagnitude, direction.magnitude);
        _characterController.anim.SetFloat(AnimLastMoveX,lastMoveDirection.x);
       _characterController.anim.SetFloat(AnimLastMoveY,lastMoveDirection.y);
       _characterController.anim.SetFloat(AnimaAttackX,attackDirection.x);
       _characterController.anim.SetFloat(AnimaAttackY,attackDirection.y);
    }
    
    
}


