using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private float speed;
    public GameObject player;
    public ParticleSystem ballDestruction;
    
    private GameObject ui;

    public AudioClip racketHit;
    public AudioClip extraBallDeath;
    public AudioClip lastBallDeath;
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI");
        
        // Haetaan GameManagerilta pallon nopeus, jotta kaikilla palloilla on varmasti sama nopeus
        speed = GameManager.manager.ballSpeed;

        // Alustetaan pallon suunta
        if(GameObject.FindGameObjectsWithTag("Ball").Length == 1)
        {
            // Jos vain yksi pallo, eli vain ainoastaan heti tason alussa
            rb2D.velocity = Vector2.up * speed;
        }
        else
        {
            // Jos uusi pallo luodaan powerupista
            rb2D.velocity = Vector2.up * speed;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Haetaan GameManagerilta pallon nopeus, jotta kaikilla palloilla on varmasti sama nopeus
        speed = GameManager.manager.ballSpeed;
        
        if(rb2D.velocity.magnitude < 1)
        {
            // Jos pallon nopeus tippuisi liian pieneksi annetaan sille hieman vauhtia
            rb2D.velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * speed;
        }

        if (Math.Abs(rb2D.velocity.y) < 0.1)
        {
            // Jos pallon nopeuden y komponentti olisi olematon, annetaan sille uusi suunta
            // Jos t‰t‰ ei olisi, pallo j‰isi v‰lill‰ jumittamaan x akselin suuntaisesti
            rb2D.velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * speed;
        }
        
        rb2D.velocity = rb2D.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && transform.position.y > player.transform.position.y)
        {
            // Osuttiin pelaajaan mailan yl‰puolelta
            // Mik‰li alapuolelta niin ei k‰ytet‰ t‰t‰ funktiota vaan kimmotaan normaalisti
            // Soitetaan ‰‰niefekti
            float volume = 0.3f;
            GameManager.manager.PlaySound(racketHit, volume);
            float hitPosition = hitSpot(transform.position, collision.transform.position, collision.collider.bounds.size.x);

            Vector2 directionVector = new Vector2(hitPosition, 1).normalized;

            rb2D.velocity = directionVector * speed;
        }

        if (collision.gameObject.CompareTag("DeathLine"))
        {
            // N‰ytet‰‰n tuhoutumis efekti
            Instantiate(ballDestruction, transform.position, Quaternion.identity);
            float volume = 1;
            if (GameObject.FindGameObjectsWithTag("Ball").Length == 1)
            {
                // Palloja vain yksi, tiputetaan el‰mi‰ yhdell‰
                GameManager.manager.lives -= 1;
                ui.GetComponent<UI>().MinusHearts();
                GameManager.manager.PlaySound(lastBallDeath, volume);
                if(GameManager.manager.lives == 0)
                {
                    // El‰m‰t loppuivat, h‰vit‰‰n peli ja menn‰‰n scoreboardille.
                    // Resetataan el‰m‰t
                    GameManager.manager.lives = 5;
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadScene>().LoadScoreboard();
                }
                transform.position = (Vector2)player.transform.position + new Vector2(0, player.transform.localScale.y / 2);
                rb2D.velocity = Vector2.up * speed;
            }
            else
            {
                // Jos palloja on enemm‰n kuin yksi, tuhotaan pallo eik‰ tiputeta el‰mi‰.
                GameManager.manager.PlaySound(extraBallDeath, volume);
                Destroy(gameObject);
            }

        }
    }

    private float hitSpot(Vector2 ballPos, Vector2 playerPos, float playerWidth)
    {
        return (2 * (ballPos.x - playerPos.x)) / playerWidth;
    }
}
