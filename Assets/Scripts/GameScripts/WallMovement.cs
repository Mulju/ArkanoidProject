using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public GameObject player;

    public float moveSpeed;
    //private bool goingLeft;
    // Start is called before the first frame update
    void Start()
    {
        //goingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Automaattinen seinän liikkuminen
        // Käydään vain jos koodi on kiinni vihreässä seinässä
        if (goingLeft)
        {
            transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(1 * moveSpeed * Time.deltaTime, 0, 0);
        }

        if (transform.position.x >= 1)
        {
            goingLeft = true;
        }
        else if (transform.position.x <= -7.5f)
        {
            goingLeft = false;
        }
        */
        gameObject.transform.position = new Vector2(player.transform.position.x, gameObject.transform.position.y);
    }
}
