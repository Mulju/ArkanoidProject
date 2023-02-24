using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class Box : MonoBehaviour
{
    public int hp;

    public GameObject powerUps;

    private string hp1 = "#CAF0F8";
    private string hp2 = "#48CAE4";
    private string hp3 = "#0077B6";
    private Color color1;
    private Color color2;
    private Color color3;

    public AudioClip boxHit;
    public AudioClip boxDestruction;

    private void Start()
    {
        // Kivat värit palikoille
        ColorUtility.TryParseHtmlString(hp1, out color1);
        ColorUtility.TryParseHtmlString(hp2, out color2);
        ColorUtility.TryParseHtmlString(hp3, out color3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {   
            // Ei tuhota laatikkoa jos laatikko osuu pelaajaan
            return;
        }

        hp--;
        float volume = 0.5f;

        switch (hp)
        {
            case 0:
                Destroy(gameObject);
                GameManager.manager.AddPoints();
                powerUps.GetComponent<PowerUps>().PowerDrop(transform);
                volume = 0.15f;
                GameManager.manager.PlaySound(boxDestruction, volume);
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().color = color1;
                GameManager.manager.PlaySound(boxHit, volume);
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = color2;
                GameManager.manager.PlaySound(boxHit, volume);
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().color = color3;
                GameManager.manager.PlaySound(boxHit, volume);
                break;
            default:
                break;
        }

        
        /*
        float powerUpRandomizer = Random.Range(0, 5);

        if(powerUpRandomizer < 1)
        {
            // Arvottiin uusi pallo peliin
            // Generoidaan uusi pallo ja haetaan sen rigidbody. Annetaan sille random suunta.
            GameObject newBall = gameObject.GetComponent<PowerUps>().Plus2Balls();
            float speed = newBall.GetComponent<Ball>().speed;
            newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * speed;
        }*/
    }
}
