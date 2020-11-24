using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animPlayer;
    private SpriteRenderer sR;
    private Rigidbody2D rB;
    private Animator sword;

    public Sprite[] current_Sprite;
    public RuntimeAnimatorController[] animCurrent;

    private GameManager gm;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public bool isAttacking;
    public Transform attackPos;
    public float attackRange;
    public int damage;
    public LayerMask whatIsEnemies;

    public float health;
    public int character;
    public float movementSpeed;

    public float playerHigh;
    public float playerLow;

    //0 = Knight, 1 = Predator, 2 = Staraptor, 3 = Mystic, 4 = Fox

    void Start()
    {
        animPlayer = GetComponent<Animator>();
        sword = GameObject.Find("Player_Attack").GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        HandleMovement(horizontal, vertical);
        HandleMovement(horizontal, vertical);
    }

    void HandleMovement(float horizontal, float vertical)
    {
        rB.velocity = new Vector2(horizontal * movementSpeed, rB.velocity.y);
        rB.velocity = new Vector2(rB.velocity.x, vertical * movementSpeed);
    }

    public void CheckStatus()
    {
        if (character == 2)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                animPlayer.SetBool("isWalkingForward", true);
            else
                animPlayer.SetBool("isWalkingForward", false);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                animPlayer.SetBool("isWalkingLeft", true);
            else
                animPlayer.SetBool("isWalkingLeft", false);

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                animPlayer.SetBool("isWalkingRight", true);
            else
                animPlayer.SetBool("isWalkingRight", false);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                animPlayer.SetBool("isWalkingBack", true);
            else
                animPlayer.SetBool("isWalkingBack", false);
        }
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
                sword.SetTrigger("isAttacking");
                isAttacking = true;

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Greed_Enemy>().TakeDamage(damage);
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
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
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