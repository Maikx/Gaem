using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Animator anim;
    private Rigidbody2D rB;
    public float movementSpeed;
    public bool isGrounded;
    public GameObject highPoint;
    public GameObject lowPoint;
    public GameObject leftPoint;
    public GameObject rightPoint;

    void Start()
    {
        anim = GameObject.Find("Staraptor").GetComponent<Animator>();
        rB = GetComponent<Rigidbody2D>();
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
        float vertical =   Input.GetAxis("Vertical");
        HandleMovement(horizontal,vertical);
        HandleMovement(horizontal, vertical);
    }

    void HandleMovement(float horizontal, float vertical)
    {
        rB.velocity = new Vector2 (horizontal * movementSpeed, rB.velocity.y);
        rB.velocity = new Vector2 (rB.velocity.x,vertical * movementSpeed);
    }

    public void CheckStatus()
    {
        if (Input.GetKey(KeyCode.S))
            anim.SetBool("isWalkingForward", true);
        else
            anim.SetBool("isWalkingForward", false);
    }
    void Boundaries()
    {
        if (transform.position.y > highPoint.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, highPoint.transform.position.y, transform.position.z);
        }
        if (transform.position.y < lowPoint.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, lowPoint.transform.position.y, transform.position.z);
        }
        if (transform.position.x < leftPoint.transform.position.x)
        {
            transform.position = new Vector3(leftPoint.transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightPoint.transform.position.x)
        {
            transform.position = new Vector3(rightPoint.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}