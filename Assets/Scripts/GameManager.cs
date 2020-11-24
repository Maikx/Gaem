using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player_S;
    private GameObject player_GO;
    private GameObject viewPoint_GO;
    private Camera viewPoint_S;
    private GameObject boundaries_GO;
    public bool canChangeScene;
    public int scene_number;
    private int current_scene;

    void Start()
    {
        player_S = GameObject.Find("Player").GetComponent<Player>();
        player_GO = GameObject.Find("Player");
        viewPoint_GO = GameObject.Find("Camera");
        viewPoint_S = GameObject.Find("Camera").GetComponent<Camera>();
        boundaries_GO = GameObject.Find("Boundaries");
        current_scene = scene_number;
        canChangeScene = true;
    }

    void FixedUpdate()
    {
        CheckScene();
    }

    void CheckScene()
    {
        if (current_scene != scene_number)
        {
            MoveToScene();
            current_scene = scene_number;
        }
    }

    void MoveToScene()
    {
        if (scene_number == 0)
        {
            if (current_scene == 1)
            {
                boundaries_GO.transform.position = new Vector3(boundaries_GO.transform.position.x - 19.2f, boundaries_GO.transform.position.y, boundaries_GO.transform.position.z);
                player_GO.transform.position = new Vector3(player_GO.transform.position.x - 2.5f, player_GO.transform.position.y, player_GO.transform.position.z);
                viewPoint_GO.transform.position = new Vector3(viewPoint_GO.transform.position.x - 19.2f, viewPoint_GO.transform.position.y, viewPoint_GO.transform.position.z);
            }
            if(current_scene == -1)
            {
                boundaries_GO.transform.position = new Vector3(boundaries_GO.transform.position.x +19.2f, boundaries_GO.transform.position.y, boundaries_GO.transform.position.z);
                player_GO.transform.position = new Vector3(player_GO.transform.position.x + 2.5f, player_GO.transform.position.y, player_GO.transform.position.z);
                viewPoint_GO.transform.position = new Vector3(viewPoint_GO.transform.position.x + 19.2f, viewPoint_GO.transform.position.y, viewPoint_GO.transform.position.z);
            }
        }
        else
        {
                boundaries_GO.transform.position = new Vector3(boundaries_GO.transform.position.x + 19.2f * scene_number, boundaries_GO.transform.position.y, boundaries_GO.transform.position.z);
                player_GO.transform.position = new Vector3(player_GO.transform.position.x + 2.5f * scene_number, player_GO.transform.position.y, player_GO.transform.position.z);
                viewPoint_GO.transform.position = new Vector3(viewPoint_GO.transform.position.x + 19.2f * scene_number, viewPoint_GO.transform.position.y, viewPoint_GO.transform.position.z);
        }
    }
}
