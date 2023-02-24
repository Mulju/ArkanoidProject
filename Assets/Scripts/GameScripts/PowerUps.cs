using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUps : MonoBehaviour
{
    public GameObject ball;

    public GameObject extraBallDrop;
    public GameObject lengthDrop;
    public GameObject speedDrop;
    public GameObject extraLifeDrop;

    private GameObject ui;
    // Start is called before the first frame update

    public AudioClip anotherBallSound;
    public AudioClip anotherLifeSound;
    public AudioClip racketLengthSound;
    public AudioClip moreTimeSound;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PowerDrop(Transform transform)
    {
        int powerUpRandomizer = Random.Range(0, 20);
        GameObject instantiated;
        float ballSpeed = GameManager.manager.ballSpeed / 2;

        if (powerUpRandomizer == 11 || powerUpRandomizer == 12)
        {
            // Arvottiin uusi pallo peliin
            instantiated = Instantiate(extraBallDrop, transform.position, Quaternion.identity);
            instantiated.GetComponent<Rigidbody2D>().velocity = Vector2.down * ballSpeed;
        }
        else if (powerUpRandomizer == 13 || powerUpRandomizer == 14)
        {
            // Arvottiin mailan pituutta
            instantiated = Instantiate(lengthDrop, transform.position, Quaternion.identity);
            instantiated.GetComponent<Rigidbody2D>().velocity = Vector2.down * ballSpeed;
        }
        else if (powerUpRandomizer == 15 || powerUpRandomizer == 16)
        {
            // Arvottiin pallon(?, luki ennen mailan) nopeutta
            instantiated = Instantiate(speedDrop, transform.position, Quaternion.identity);
            instantiated.GetComponent<Rigidbody2D>().velocity = Vector2.down * ballSpeed;
        }
        else if (powerUpRandomizer == 17)
        {
            // Arvottiin extra el‰m‰
            instantiated = Instantiate(extraLifeDrop, transform.position, Quaternion.identity);
            instantiated.GetComponent<Rigidbody2D>().velocity = Vector2.down * ballSpeed;
        }
        else
        {
            // Ei tehd‰ mit‰‰n, pelaaja ei saa poweruppia
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Osuttiin pelaajaan
            Destroy(gameObject);
            GameManager.manager.AddPoints();
            float volume = 0.3f;

            if (gameObject.name == "PowerUpBall(Clone)")
            {
                Instantiate(ball, gameObject.transform.position, Quaternion.identity);
                volume = 0.5f;
                GameManager.manager.PlaySound(anotherBallSound, volume);
            }
            else if(gameObject.name == "PowerUpLength(Clone)")
            {
                // Kasvatetaan mailan kokoa
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                volume = 1;
                GameManager.manager.PlaySound(racketLengthSound, volume);
                if (playerTransform.localScale.x < 5f)
                {
                    // Jos pelaajan maila on pienempi kuin 5, kasvatetaan. Muuten pidet‰‰n samana (ettei kasva liian isoksi)
                    playerTransform.localScale += new Vector3(0.3f, 0f, 0f);
                }
            }
            else if(gameObject.name == "PowerUpLife(Clone)")
            {
                volume = 1;
                GameManager.manager.PlaySound(anotherLifeSound, volume);
                if (GameManager.manager.lives < 9)
                {
                    // Ei menn‰ yli yhdeks‰n lifen
                    GameManager.manager.lives++;
                    ui.GetComponent<UI>().PlusHearts();
                }
            }
            else if(gameObject.name == "PowerUpSpeed(Clone)")
            {
                GameManager.manager.PlaySound(moreTimeSound, volume);
                // Tyhm‰ nimi, mutta yritin pit‰‰ ne konsistentteina. Tarkoituksena pienent‰‰ pallon nopeutta.
                if (GameManager.manager.ballSpeed > 7)
                {
                    // Jos pallon nopeus on riitt‰v‰n suuri, piennennet‰‰n sit‰ hiukan.
                    // En halua sen menev‰n alle 5:n kuitenkaan.
                    GameManager.manager.ballSpeed -= 3;
                }
            }
        }
    }
}
