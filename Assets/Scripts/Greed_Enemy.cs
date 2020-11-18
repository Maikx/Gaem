using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greed_Enemy : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;
    public float health;

    private Transform size;
    private Transform target;
    
    static Animator animGreed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animGreed = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        CheckIfDead();
    }

    void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, target.position) > attackRange)
        {
            animGreed.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            AttackPlayer();
            animGreed.SetBool("isWalking", false);
        }

        if(target.position.x < transform.position.x)
        {
            transform.eulerAngles = Vector3.up * 180;
        }
        else
        {
            transform.eulerAngles = Vector3.up * 0;
        }

        if (target.position.y < transform.position.y)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, 0.1f);
        }
        else
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        }
    }

    void AttackPlayer()
    {
            if (Time.time > lastAttackTime + attackDelay)
            {
                target.SendMessage("TakeDamage", damage);
                lastAttackTime = Time.time;
            }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("damage TAKEN !");
        }
    }

    public void CheckIfDead()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
