using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Tutoriaali koodia käytetty
// https://gamedevbeginner.com/how-to-keep-score-in-unity-with-loading-and-saving/
public class HighScoreDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text scoreText;
    public void DisplayHighScore(string name, int score)
    {
        nameText.text = name;
        scoreText.text = string.Format("{0:000000}", score);
    }
    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }
}
