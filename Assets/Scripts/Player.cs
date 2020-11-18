using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Animator animStaraptor;
    private Rigidbody2D rB;
    private GameManager gm;
    private Animator sword;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public int damage;
    public LayerMask whatIsEnemies;


    public float movementSpeed;
    //public int meleeDamage;
    //public float meleeRange;
    public float health;


    public int character;

    //0 = Knight, 1 = Predator, 2 = Staraptor, 3 = Mystic, 4 = Fox

    void Start()
    {
        
        if(character == 2)animStaraptor = GameObject.Find("Staraptor").GetComponent<Animator>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sword = GameObject.Find("AttackPos").GetComponent<Animator>();
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        Boundaries();
        Attack();
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
                animStaraptor.SetBool("isWalkingForward", true);
            else
                animStaraptor.SetBool("isWalkingForward", false);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                animStaraptor.SetBool("isWalkingLeft", true);
            else
                animStaraptor.SetBool("isWalkingLeft", false);

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                animStaraptor.SetBool("isWalkingRight", true);
            else
                animStaraptor.SetBool("isWalkingRight", false);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                animStaraptor.SetBool("isWalkingBack", true);
            else
                animStaraptor.SetBool("isWalkingBack", false);
        }
    }

    void Boundaries()
    {
        if (transform.position.y > 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
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
        if(timeBtwAttack <= 0)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                sword.SetTrigger("isAttacking");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Greed_Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
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