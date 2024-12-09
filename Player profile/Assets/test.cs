using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    GameObject player;
    PlayerProfile playerProfile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerProfile = player.GetComponent<PlayerProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerProfile.addWin(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            playerProfile.addLosses(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerProfile.addScore(1, 3);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            playerProfile.addScore(3, 1);
        }
    }
}
