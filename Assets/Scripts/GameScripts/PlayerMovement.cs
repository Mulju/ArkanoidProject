using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed;
    private float gravityMultiplier;

    private float currentPoint;
    private float previousPoint;
    private float highPoint;

    private bool goingDown;

    public int jumpForce;
    public Rigidbody2D rb2D;

    public float dampMultiplier;

    public Transform leftCheckPosition;
    public Transform rightCheckPosition;
    public float checkRadius;
    public LayerMask checkLayer;

    private bool goingLeft;

    public AudioClip jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2( 0, -9.81f);
        previousPoint = 0;
        highPoint = 1;
        goingDown = true;
        
        goingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("ColoredWallGreen"))
        {
            // K‰yd‰‰n vain jos koodi on kiinni vihre‰ss‰ sein‰ss‰
            if (goingLeft)
            {
                transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(0, 1 * moveSpeed * Time.deltaTime, 0);
            }

            if(transform.position.x <= 1)
            {
                goingLeft = true;
            }
            else if(transform.position.x >= -7.5f)
            {
                goingLeft = false;
            }
            return;
        }

        if (Physics2D.OverlapCircle(leftCheckPosition.position, checkRadius, checkLayer))
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                // Pelaaja yritt‰‰ menn‰ vasemmalle, vaikka tiell‰ on este. Ei tehd‰ mit‰‰n
            }
            else
            {
                // Pelaaja saa liikkua normaalisti
                transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
            }
        }
        else if (Physics2D.OverlapCircle(rightCheckPosition.position, checkRadius, checkLayer))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                // Pelaaja yritt‰‰ menn‰ oikealle, vaikka tiell‰ on este. Ei tehd‰ mit‰‰n
            }
            else
            {
                // Pelaaja saa liikkua normaalisti
                transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            // Ei ole esteit‰, annetaan pelaajan liikkua normaalisti
            transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        }

        DampedHarmonicOscillation();

        if (Input.GetButtonDown("Jump") && Math.Abs(currentPoint) <= 0.3)
        {
            float volume = 1;
            GameManager.manager.PlaySound(jumpSound, volume);
            rb2D.velocity = new Vector2(0, jumpForce);
        }
    }

    /* 
     * Funktio hiljennetylle harmoniselle v‰r‰htelij‰lle tasoittamaan pelaajan heiluminen
     * tasapainorajan yli.
     */
    private void DampedHarmonicOscillation()
    {
        currentPoint = transform.position.y;
        
        if(-0.1 < currentPoint && currentPoint < 0.1)
        {
            if(Math.Abs(rb2D.velocity.y) < 0.05)
            {
                rb2D.velocity = new Vector2(0, 0);
            }
        }

        if (currentPoint > 0)
        {
            // Ollaan painovoima rajan yl‰puolella, eli y arvo positiivsta
            if (currentPoint < previousPoint && goingDown == false)
            {
                // Aletaan menn‰ alasp‰in
                highPoint = previousPoint;
                goingDown = true;
            }
            
            gravityMultiplier = -9.81f;

            if (goingDown)
            {
                // Tuottaa annetuilla arvoilla hiljennetyn harmonisen v‰r‰htelij‰n jarruttamalla liikett‰
                gravityMultiplier = -9.81f * (currentPoint - highPoint / dampMultiplier);
            }
            Physics2D.gravity = new Vector2(0, gravityMultiplier);
        }
        else
        {
            // Ollaan painovoima rajan alapuolella, eli y arvo negatiivinen
            if (currentPoint > previousPoint && goingDown == true)
            {
                // Menossa ylˆsp‰in
                highPoint = previousPoint;
                goingDown = false;
            }

            gravityMultiplier = 9.81f;

            if (goingDown == false)
            {
                // Tuottaa annetuilla arvoilla hiljennetyn harmonisen v‰r‰htelij‰n jarruttamalla liikett‰
                gravityMultiplier = 9.81f * (Math.Abs(currentPoint) - Math.Abs(highPoint) / dampMultiplier);
            }

            Physics2D.gravity = new Vector2(0, gravityMultiplier);
        }

        previousPoint = currentPoint;
    }
}
