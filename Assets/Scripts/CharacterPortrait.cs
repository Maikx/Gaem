using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{

    public Image image;

    public Sprite[] playerSprite;

    private Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        if (player.character == 0)
            image.sprite = playerSprite[0];
        if (player.character == 1)
            image.sprite = playerSprite[1];
        if (player.character == 2)
            image.sprite = playerSprite[2];
        if (player.character == 3)
            image.sprite = playerSprite[3];
        if (player.character == 4)
            image.sprite = playerSprite[4];
    }
}
