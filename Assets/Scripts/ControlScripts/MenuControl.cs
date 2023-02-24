using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuControl : MonoBehaviour
{
    private EventSystem eventSystem;
    private Resolution[] resolutions;
    public TMP_Dropdown resolutionsDropdown;

    private string currentScene;

    void Start()
    {
        eventSystem = EventSystem.current;
        resolutions = Screen.resolutions;

        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu")
        {
            // Vain main menussa on resoluution vaihto mahdollisuus
            resolutionsDropdown.ClearOptions();
            List<string> options = new List<string>();

            int resolutionIndex = 0;
            int i = 0;
            foreach (Resolution resolution in resolutions)
            {
                // K‰‰nteisess‰ j‰rjestyksess‰ niin n‰ytt‰‰ n‰timm‰lt‰
                string option = resolution.width + " x " + resolution.height;
                options.Add(option);

                // Alla oleva if jotta valitaan sama resoluutio mik‰ k‰ytt‰j‰ll‰
                // on t‰ll‰ hetkell‰ koneessa valittuna
                if (resolution.width == Screen.width &&
                    resolution.height == Screen.height)
                {
                    resolutionIndex = i;
                }
                i++;
            }

            resolutionsDropdown.AddOptions(options);
            resolutionsDropdown.value = resolutionIndex;
            resolutionsDropdown.RefreshShownValue();
        }
    }

    public void SelectActive(GameObject selected)
    {
        eventSystem.SetSelectedGameObject(selected);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void StoreName(string name)
    {
        GameManager.manager.playerName = name;
    }
}
