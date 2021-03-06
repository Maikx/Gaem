﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animPlayer;
    private SpriteRenderer sR;
    private Rigidbody2D rB;
    public Animator[] sword;

    private GameManager gm;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public bool isAttacking;
    public Transform[] attackPos;
    public float attackRange;
    public int damage;
    public LayerMask whatIsEnemies;
    public Vector3 direction;

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    public int character;
    public int currentcharacter;


    public float movementSpeed;

    public float playerHigh;
    public float playerLow;

    public float horizontal;
    public float vertical;

    public bool willIdleL;
    public bool willIdleR;
    public bool isMoving;

    public float forwardAttackY = -3.5f;

    //0 = Knight, 1 = Predator, 2 = Staraptor, 3 = Mystic, 4 = Fox

    void Start()
    {
        animPlayer = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rB = GetComponent<Rigidbody2D>();
        willIdleR = true;
        animPlayer.SetInteger("Character", character);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentcharacter = character;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
        CheckDirection();
        ChangeCharacter();
        Boundaries();
        Attack();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        HandleMovement(horizontal, vertical);
        animPlayer.SetFloat("Horizontal", horizontal);
    }

    void HandleMovement(float horizontal, float vertical)
    {
        rB.velocity = new Vector2(horizontal * movementSpeed, rB.velocity.y);
        rB.velocity = new Vector2(rB.velocity.x, vertical * movementSpeed);
    }

    void CheckDirection()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            willIdleL = true;
            willIdleR = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            willIdleR = true;
            willIdleL = false;
        }

            if (horizontal != 0 || vertical != 0)
            isMoving = true;
        else
            isMoving = false;
    }

    void ChangeCharacter()
    {
        if(currentcharacter != character)
        {
            animPlayer.SetTrigger("Change");
            animPlayer.SetInteger("Character", character);
            currentcharacter = character;
        }

        if (Input.GetKeyDown(KeyCode.F1))
            character = 0;

        if (Input.GetKeyDown(KeyCode.F2))
            character = 1;

        if (Input.GetKeyDown(KeyCode.F3))
            character = 2;

        if (Input.GetKeyDown(KeyCode.F4))
            character = 3;

        if (Input.GetKeyDown(KeyCode.F5))
            character = 4;
    }

    public void CheckAnimation()
    {
            if (isMoving == false && willIdleL == true)
            {
            animPlayer.SetBool("isIdleLeft", true);
            animPlayer.SetFloat("Horizontal", -1);
            }

            else
            animPlayer.SetBool("isIdleLeft", false);

            if (isMoving == true)
            animPlayer.SetBool("isWalking", true);
            else
            animPlayer.SetBool("isWalking", false);

            if (isMoving == false && willIdleR == true)
            {
            animPlayer.SetBool("isIdleRight", true);
            animPlayer.SetFloat("Horizontal", 1);
            }

            else
            animPlayer.SetBool("isIdleRight", false);

            if(isMoving == true && willIdleL == true)
            animPlayer.SetFloat("Horizontal", -1);
        
            if (isMoving == true && willIdleR == true)
            animPlayer.SetFloat("Horizontal", 1);
    }
    
    void Boundaries()
    {
        if (transform.position.y > playerHigh)
        {
            transform.position = new Vector3(transform.position.x, playerHigh, transform.position.z);
        }
        if (transform.position.y < playerLow)
        {
            transform.position = new Vector3(transform.position.x, playerLow, transform.position.z);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animPlayer.SetTrigger("isHurt");
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void Attack()
    {
        if (isAttacking == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {



                if (willIdleL == true && willIdleR == false)
                {
                    sword[0].SetTrigger("isAttacking");
                    isAttacking = true;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos[0].position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Greed_Enemy>().TakeDamage(damage);
                    }
                }

                if (willIdleL == false && willIdleR == true)
                {
                    sword[1].SetTrigger("isAttacking");
                    isAttacking = true;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos[1].position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Greed_Enemy>().TakeDamage(damage);
                    }
                }
            }
        }

        if (isAttacking == true)
        {
            timeBtwAttack -= Time.deltaTime;
            if (timeBtwAttack <= 0)
            {
                timeBtwAttack = startTimeBtwAttack;
                isAttacking = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos[0].position, attackRange);
        Gizmos.DrawWireSphere(attackPos[1].position, attackRange);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Right")
        {
            gm.scene_number ++;
        }

        if (col.gameObject.tag == "Left")
        {
            gm.scene_number --;
        }
    }
}