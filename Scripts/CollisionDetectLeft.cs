using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectLeft : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerScript;
    public bool detect;

    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        detect = true;

        if (col.gameObject.layer == 8)
        {
            print("oui");
            playerScript.Lblocked = true;
        }
        else
        {
            playerScript.Lblocked = false;
        }
    }
}
