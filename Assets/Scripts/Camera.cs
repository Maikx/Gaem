using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public bool moveCamera;
    public GameObject viewPoint;
    private GameManager gm;

    void Start()

    {
        viewPoint = GameObject.Find("Camera");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
