using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animPlayer;
    private SpriteRenderer sR;
    private Rigidbody2D rB;

    public Sprite staraptor_Sprite;
    public RuntimeAnimatorController animStaraptor;

    public Animator animPredator;
    public Sprite predator_Sprite;

    private GameManager gm;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public int damage;
    public LayerMask whatIsEnemies;
    
    //public Animator vet[4];
    //public Animator[] vet = new Animator[4];

    public float health;
    public int character;
    public float movementSpeed;

    public float playerHigh;
    public float playerLow;

    //cose già aggiunte..

    //public int meleeDamage;
    //public float meleeRange;

    //0 = Knight, 1 = Predator, 2 = Staraptor, 3 = Mystic, 4 = Fox

    void Start()
    {
        animPlayer = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
        //vet[1]=animStaraptor = GameObject.Find("Staraptor").GetComponent<Animator>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // CheckStatus();
        Boundaries();
        PickCharacter();
        Attack();
    }

    void PickCharacter()
    {
        if (character == 0)
        {
            //knight
        }

        if (character == 1)
        {
            //predator
        }

        if (character == 2)
        {
            sR.sprite = staraptor_Sprite;
            animPlayer.runtimeAnimatorController = animStaraptor;
        }

        if (character == 3)
        {
            //mystic
        }

        if (character == 4)
        {
            //fox
        }

    }


    //int PickCharacter()
    //{
        //character = 2;
        //return character;
    //}


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
        //int pos = PickCharacter();




        if (character == 2)
        {
            //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                //vet[pos].SetBool("isWalkingForward", true);
            //else
                //vet[pos].SetBool("isWalkingForward", false);

            //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                //vet[pos].SetBool("isWalkingLeft", true);
            //else
                //vet[pos].SetBool("isWalkingLeft", false);

            //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                //vet[pos].SetBool("isWalkingRight", true);
            //else
                //vet[pos].SetBool("isWalkingRight", false);
            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                //vet[pos].SetBool("isWalkingBack", true);
            //else
                //vet[pos].SetBool("isWalkingBack", false);
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
        if(timeBtwAttack <= 0)
        {
            if(Input.GetKey(KeyCode.Space))
            {
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