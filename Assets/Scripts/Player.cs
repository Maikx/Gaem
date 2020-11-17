using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Animator animStaraptor;
    private Rigidbody2D rB;
    private GameManager gm;
    public float movementSpeed;
    private GameObject highPoint_GO;
    private GameObject lowPoint_GO;


    public int character;

    //0 = Knight, 1 = Predator, 2 = Staraptor, 3 = Mystic, 4 = Fox

    void Start()
    {
        
        if(character == 2)animStaraptor = GameObject.Find("Staraptor").GetComponent<Animator>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rB = GetComponent<Rigidbody2D>();
        highPoint_GO = GameObject.Find("High");
        lowPoint_GO = GameObject.Find("Low");
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        Boundaries();
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
        if (transform.position.y > highPoint_GO.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, highPoint_GO.transform.position.y, transform.position.z);
        }
        if (transform.position.y < lowPoint_GO.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, lowPoint_GO.transform.position.y, transform.position.z);
        }
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