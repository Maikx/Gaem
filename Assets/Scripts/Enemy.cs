using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Transform size;
    private Transform target;
    
    static Animator animGreed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animGreed = GameObject.Find("Greed").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, target.position) > 2)
        {
            animGreed.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
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
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.1f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        }
    }
}
