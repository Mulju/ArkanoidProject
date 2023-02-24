using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GUIStyle myStyle;

    public MenuControl menuScript;
    private GameObject pauseMenu;
    private bool gameIsPaused;
    private GameObject masterSlider;
    private EventSystem eventSystem;
    private GameObject score;

    [SerializeField]
    private GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        gameIsPaused = false;
        myStyle.fontSize = 30;
        myStyle.normal.textColor = Color.red;

        score = GameObject.FindGameObjectWithTag("Score");

        for(int i = 5; i < hearts.Length; i++)
        {
            // Laitetaan extra elämät pois päältä
            hearts[i].SetActive(false);
        }

        // Alla olevat ESC:llä pausettamista varten
        masterSlider = GameObject.FindGameObjectWithTag("MasterVol");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score.GetComponent<Text>().text = GameManager.manager.points.ToString();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // Pausetus toimii myös ESC:llä
            PauseGame();
            if (gameIsPaused)
            {
                pauseMenu.SetActive(true);
                eventSystem.SetSelectedGameObject(masterSlider);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }
    }

    /*
     * Ei enää käytössä oleva
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 50), "Lives: " + GameManager.manager.lives, myStyle);
        GUI.Label(new Rect(10, 60, 200, 50), "Points: " + GameManager.manager.points, myStyle);
    }*/

    public void PauseGame()
    {
        // Pitää huolen myös pelin jatkamisesta
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gameIsPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            gameIsPaused = true;
        }
    }

    public void PlusHearts()
    {
        hearts[GameManager.manager.lives - 1].SetActive(true);
    }

    public void MinusHearts()
    {
        hearts[GameManager.manager.lives].SetActive(false);
    }
}
