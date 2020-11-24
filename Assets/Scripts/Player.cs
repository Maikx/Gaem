using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animPlayer;
    private SpriteRenderer sR;
    private Rigidbody2D rB;
    public Animator[] sword;


    public Sprite[] current_Sprite;
    public RuntimeAnimatorController[] animCurrent;

    private GameManager gm;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public bool isAttacking;
    public Transform[] attackPos;
    public float attackRange;
    public int damage;
    public LayerMask whatIsEnemies;

    public float health;
    public int character;
    public float movementSpeed;

    public float playerHigh;
    public float playerLow;

    public float horizontal;
    public float vertical;

    public bool willIdleB;
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
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
        CheckDirection();
        Boundaries();
        PickCharacter();
        Attack();
    }

    void PickCharacter()
    {
        if (character == 0)
        {
            sR.sprite = current_Sprite[0];
            animPlayer.runtimeAnimatorController = animCurrent[0];
        }

        if (character == 1)
        {
            sR.sprite = current_Sprite[1];
            animPlayer.runtimeAnimatorController = animCurrent[1];
        }

        if (character == 2)
        {
            sR.sprite = current_Sprite[2];
            animPlayer.runtimeAnimatorController = animCurrent[2];
        }

        if (character == 3)
        {
            sR.sprite = current_Sprite[3];
            animPlayer.runtimeAnimatorController = animCurrent[3];
        }

        if (character == 4)
        {
            sR.sprite = current_Sprite[4];
            animPlayer.runtimeAnimatorController = animCurrent[4];
        }

    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        HandleMovement(horizontal, vertical);
        HandleMovement(horizontal, vertical);
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
            willIdleB = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            willIdleR = true;
            willIdleL = false;
            willIdleB = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            willIdleB = true;
            willIdleL = false;
            willIdleR = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            willIdleB = false;
            willIdleL = false;
            willIdleR = false;
        }

            if (horizontal != 0 || vertical != 0)
            isMoving = true;
        else
            isMoving = false;
    }

    public void CheckAnimation()
    {
        
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                animPlayer.SetBool("isWalkingForward", true);
            }
            else
            {
                animPlayer.SetBool("isWalkingForward", false);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                animPlayer.SetBool("isWalkingLeft", true);
            }
            else
            {
                animPlayer.SetBool("isWalkingLeft", false);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animPlayer.SetBool("isWalkingRight", true);
            }
            else
            {
                animPlayer.SetBool("isWalkingRight", false);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                animPlayer.SetBool("isWalkingBack", true);
            }
            else
            {
                animPlayer.SetBool("isWalkingBack", false);
            }

            if(isMoving == false && willIdleL == true)
            animPlayer.SetBool("isIdleLeft", true);

            else
            animPlayer.SetBool("isIdleLeft", false);

            if (isMoving == false && willIdleR == true)
            animPlayer.SetBool("isIdleRight", true);

            else
            animPlayer.SetBool("isIdleRight", false);

            if (isMoving == false && willIdleB == true)
            animPlayer.SetBool("isIdleBack", true);

            else
            animPlayer.SetBool("isIdleBack", false);
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
        health -= damage;
        if(health <= 0)
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
                if (willIdleB == false && willIdleL == false && willIdleR == false)
                {
                    sword[0].SetTrigger("isAttacking");
                    isAttacking = true;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos[0].position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Greed_Enemy>().TakeDamage(damage);
                    }
                }

                if (willIdleB == true && willIdleL == false && willIdleR == false)
                {
                    sword[1].SetTrigger("isAttacking");
                    isAttacking = true;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos[1].position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Greed_Enemy>().TakeDamage(damage);
                    }
                }

                if (willIdleB == false && willIdleL == true && willIdleR == false)
                {
                    sword[2].SetTrigger("isAttacking");
                    isAttacking = true;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos[2].position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Greed_Enemy>().TakeDamage(damage);
                    }
                }

                if (willIdleB == false && willIdleL == false && willIdleR == true)
                {
                    sword[3].SetTrigger("isAttacking");
                    isAttacking = true;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos[2].position, attackRange, whatIsEnemies);
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
        Gizmos.DrawWireSphere(attackPos[2].position, attackRange);
        Gizmos.DrawWireSphere(attackPos[3].position, attackRange);
        
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