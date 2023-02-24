using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Color = UnityEngine.Color;

public class RacketColour : MonoBehaviour
{
    private Color[] colors;
    private int colorIndex;
    public int wallIndex;

    private string purple = "#D512E0";
    private Color colorPurple;

    public AudioClip colourSound;
    private float volume = 0.3f;
    private void Start()
    {
        ColorUtility.TryParseHtmlString(purple, out colorPurple);

        colors = new Color[3] {Color.red, colorPurple, Color.green};
        colorIndex = 0;
        wallIndex = 0;

        // Jos scripti on sein‰ss‰ kiinni, asetetaan sille oma indeksi
        if (gameObject.CompareTag("ColoredWallRed"))
        {
            wallIndex = 0;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (gameObject.CompareTag("ColoredWallPurple"))
        {
            wallIndex = 1;
            gameObject.GetComponent<Renderer>().material.color = colorPurple;
            // Disabloidaan muiden v‰rien colliderit
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (gameObject.CompareTag("ColoredWallGreen"))
        {
            wallIndex = 2;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            // Disabloidaan muiden v‰rien colliderit
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }
    void Update()
    {
        // Seuraava v‰ri
        if(Input.GetButtonDown("Racket Color Next"))
        {
            // Jos viimeinen v‰ri listasta, hyp‰t‰‰n listan alkuun
            if (colorIndex == colors.Length - 1)
            {
                colorIndex = 0;
            }
            else
            {
                colorIndex++;
            }

            NextColor();
        }

        // Edellinen v‰ri
        if(Input.GetButtonDown("Racket Color Previous"))
        {
            // Jos ensimm‰inen v‰ri listassa, hyp‰t‰‰n listan loppuun
            if (colorIndex == 0)
            {
                colorIndex = colors.Length - 1;
            }
            else
            {
                colorIndex--;
            }

            PreviousColor();
        }

        // Punainen v‰ri
        if(Input.GetButtonDown("Racket Color Red"))
        {
            colorIndex = 0;

            // Onko scripti pelaajassa vai v‰ritetyss‰ sein‰ss‰ kiinni
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<Renderer>().material.color = colors[colorIndex];
                GameManager.manager.PlaySound(colourSound, volume);
            }
            else
            {
                ChangeColor();
            }
        }

        // Liila v‰ri
        if (Input.GetButtonDown("Racket Color Purple"))
        {
            colorIndex = 1;

            // Onko scripti pelaajassa vai v‰ritetyss‰ sein‰ss‰ kiinni
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<Renderer>().material.color = colors[colorIndex];
                GameManager.manager.PlaySound(colourSound, volume);
            }
            else
            {
                ChangeColor();
            }
        }
        
        // Vihre‰ v‰ri
        if (Input.GetButtonDown("Racket Color Green"))
        {
            colorIndex = 2;

            // Onko scripti pelaajassa vai v‰ritetyss‰ sein‰ss‰ kiinni
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<Renderer>().material.color = colors[colorIndex];
                GameManager.manager.PlaySound(colourSound, volume);
            }
            else
            {
                ChangeColor();
            }
        }
    }

    public void NextColor()
    {
        // Onko scripti pelaajassa vai v‰ritetyss‰ sein‰ss‰ kiinni
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Renderer>().material.color = colors[colorIndex];
            GameManager.manager.PlaySound(colourSound, volume);
        }
        else
        {
            ChangeColor();
        }
    }

    public void PreviousColor()
    {
        // Onko scripti pelaajassa vai v‰ritetyss‰ sein‰ss‰ kiinni
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Renderer>().material.color = colors[colorIndex];
            GameManager.manager.PlaySound(colourSound, volume);
        }
        else
        {
            ChangeColor();
        }
    }

    public void ChangeColor()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        if (colorIndex == wallIndex)
        {
            // Alpha channel t‰ysiin ja collider p‰‰lle
            tmp.a = 1f;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            // Alpha channel puoleen ja collider pois p‰‰lt‰
            tmp.a = 0.5f;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }
}
