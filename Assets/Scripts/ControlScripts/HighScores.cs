using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Tutoriaalin koodia käytetty
// https://gamedevbeginner.com/how-to-keep-score-in-unity-with-loading-and-saving/
public class HighScores : MonoBehaviour
{
    public HighScoreDisplay[] highScoreDisplayArray;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();

    public Text yourScore;
    void Start()
    {
        scores = GameManager.manager.Load();
        AddNewScore(GameManager.manager.playerName, GameManager.manager.points);
        UpdateDisplay();
        GameManager.manager.Save(scores);

        yourScore.text = GameManager.manager.points.ToString();
    }
    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));
        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].name, scores[i].score);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }
    void AddNewScore(string entryName, int entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
    }
}
