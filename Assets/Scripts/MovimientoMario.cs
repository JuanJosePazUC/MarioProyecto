﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMario : MonoBehaviour
{

    private float walkSpeed = 5f;
    private Animator animator;
    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    public float jumpForce = 850;
    private int groundMask;
    public bool isGrounded;
    public bool isJumping;
    private string currentAnimaton;
    private bool isAttackPressed;
    private bool isAttacking;
    private float attackDelay = 0.3f;
    public float jumpTimeCounter;
    public float jumpTime;
    public float groundCheck;


    //Animation States
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";
    const string PLAYER_JUMP = "Player_jump";
    const string PLAYER_ATTACK = "Player_attack";
    const string PLAYER_AIR_ATTACK = "Player_air_attack";

    //=====================================================
    // Start is called before the first frame update
    //=====================================================
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");

    }

    //=====================================================
    // Update is called once per frame
    //=====================================================
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheck, groundMask);
        
        //Checking for inputs
        xAxis = Input.GetAxisRaw("Horizontal");

        //space jump key pressed?
        Salto();

        //space Atatck key pressed?
        if (Input.GetKeyDown(KeyCode.C))
        {
            isAttackPressed = true;
        }
    }

    //=====================================================
    // Physics based time step loop
    //=====================================================
    private void FixedUpdate()
    {
        //------------------------------------------

        //Check update movement based on input
        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);
            
        }
        else
        {
            vel.x = 0;
            
        }

        if (isGrounded && !isAttacking)
        {
            if (xAxis != 0)
            {
                //ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                //ChangeAnimationState(PLAYER_IDLE);
            }
        }

        //------------------------------------------


        //assign the new velocity to the rigidbody
        rb2d.velocity = vel;


        //attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;

                if(isGrounded)
                {
                    //ChangeAnimationState(PLAYER_ATTACK);
                }
                else
                {
                    //ChangeAnimationState(PLAYER_AIR_ATTACK);
                }

  
                Invoke("AttackComplete", attackDelay);
                
            }


        }

    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void Salto(){
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb2d.velocity = Vector2.up * jumpForce;
        }
        if(Input.GetKey(KeyCode.Space) && isJumping==true){
            if(jumpTimeCounter > 0){
                rb2d.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -=Time.deltaTime; 
            } else{
                isJumping = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            isJumping = false;
        }
    }

    //=====================================================
    // mini animation manager
    //=====================================================
    /*
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
    */

}
