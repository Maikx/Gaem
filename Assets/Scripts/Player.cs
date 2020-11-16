using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Animator anim;
    private Rigidbody2D rB;
    public float movementSpeed;

    void Start()
    {
        anim = GameObject.Find("Staraptor").GetComponent<Animator>();
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
    }

    void HandleMovement(float horizontal)
    {
        rB.velocity = new Vector2 (horizontal * movementSpeed, rB.velocity.y);
    }

    public void CheckStatus()
    {
        if (Input.GetKey(KeyCode.S))
            anim.SetBool("isWalkingForward", true);
        else
            anim.SetBool("isWalkingForward", false);
    }
}