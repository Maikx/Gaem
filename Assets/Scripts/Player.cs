using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Animator anim;
    private Rigidbody2D rB;
    private GameManager gm;
    public float movementSpeed;
    public GameObject highPoint_GO;
    public GameObject lowPoint_GO;


    void Start()
    {
        anim = GameObject.Find("Staraptor").GetComponent<Animator>();
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
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            anim.SetBool("isWalkingForward", true);
        else
            anim.SetBool("isWalkingForward", false);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            anim.SetBool("isWalkingLeft", true);
        else
            anim.SetBool("isWalkingLeft", false);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            anim.SetBool("isWalkingRight", true);
        else
            anim.SetBool("isWalkingRight", false);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            anim.SetBool("isWalkingBack", true);
        else
            anim.SetBool("isWalkingBack", false);

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