using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private string currentScene;

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Loading")
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (currentScene == "Level1")
        {
            // Scene on Level1
            GameObject[] taulu = GameObject.FindGameObjectsWithTag("Box");
            if (taulu.Length == 0)
            {
                // Taso voitettu, siirryt‰‰n seuraavaan.
                LoadLevel2();
            }
        }

        if (currentScene == "Level2")
        {
            // Scene on Level1
            GameObject[] taulu = GameObject.FindGameObjectsWithTag("Box");
            if (taulu.Length == 0)
            {
                // Taso voitettu, siirryt‰‰n seuraavaan.
                LoadLevel3();
            }
        }

        if (currentScene == "Level3")
        {
            // Scene on Level1
            GameObject[] taulu = GameObject.FindGameObjectsWithTag("Box");
            if (taulu.Length == 0)
            {
                // Taso voitettu, siirryt‰‰n seuraavaan.
                LoadLevel4();
            }
        }

        if (currentScene == "Level4")
        {
            // Scene on Level1
            GameObject[] taulu = GameObject.FindGameObjectsWithTag("Box");
            if (taulu.Length == 0)
            {
                // Taso voitettu, siirryt‰‰n seuraavaan. Lis‰t‰‰n pelin p‰‰tteeksi 100 pistett‰
                // jokaisesta j‰ljell‰ olevasta lifest‰.
                GameManager.manager.points += 100 * GameManager.manager.lives;
                LoadScoreboard();
            }
        }
    }

    public void ChangeScene()
    {
        // Alustetaan el‰m‰t aina kun vaihdetaan scene‰
        // T‰‰ pit‰‰ ehk‰ muuttaa
        GameManager.manager.lives = 5;
    }

    public void LoadMainMenu()
    {
        // Resetataan pisteet kun p‰‰dyt‰‰n MainMenuun
        GameManager.manager.points = 0;
        
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelSelection") ||
            SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scoreboard"))
        {
            // Ei aloiteta musiikkia uudestaan jos palataan Level Selectionist‰ tai Scoreboardilta
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            GameManager.manager.PlayMusic(9);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
        GameManager.manager.PlayMusic(2);

        // Asetetaan pallon nopeus 5:n joka tason alussa
        GameManager.manager.ballSpeed = 3;
    } 

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
        GameManager.manager.PlayMusic(7);
        GameManager.manager.ballSpeed = 3;
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
        GameManager.manager.PlayMusic(4);
        GameManager.manager.ballSpeed = 3;
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level4");
        GameManager.manager.PlayMusic(5);
        GameManager.manager.ballSpeed = 3;
    }
    public void LoadScoreboard()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            // Ei aloiteta musiikkia uudestaan jos tullaan Main Menusta
            SceneManager.LoadScene("Scoreboard");
        }
        else
        {
            GameManager.manager.PlayMusic(9);
            SceneManager.LoadScene("Scoreboard");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
