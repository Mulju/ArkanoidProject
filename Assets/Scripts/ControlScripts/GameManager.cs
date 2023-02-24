using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public string playerName;
    public int points;
    public int lives;
    public float ballSpeed;

    public AudioSource audioSource;
    public AudioClip particleVoyager;
    public AudioClip startYourEngine;
    public AudioClip laserGun;
    public AudioClip fasterStrongerHarder;
    public AudioClip demolitionRace;
    public AudioClip runBabyRun;
    public AudioClip blackHole;
    public AudioClip speedway;
    public AudioClip starsAndGalaxy;

    public AudioSource[] soundSources;
    public AudioClip racketHit;
    public AudioClip boxHit;
    public AudioClip boxDestruction;

    private bool level1;
    private bool level2;
    private bool level3;
    private bool level4;

    // Muuta tätä vaihtaaksesi pisteiden tippumisen aikaa
    private int secondsUntilDecrease = 15;

    public Leaderboard leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton alustus
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ballSpeed = ballSpeed + Time.deltaTime / 30;
    }

    public void PlayMusic(int i)
    {
        switch (i)
        {
            case 1:
                audioSource.clip = particleVoyager;
                break;
            case 2:
                audioSource.clip = startYourEngine;
                break;
            case 3:
                audioSource.clip = laserGun;
                break;
            case 4:
                audioSource.clip = fasterStrongerHarder;
                break;
            case 5:
                audioSource.clip = demolitionRace;
                break;
            case 6:
                audioSource.clip = runBabyRun;
                break;
            case 7:
                audioSource.clip = blackHole;
                break;
            case 8:
                audioSource.clip = speedway;
                break;
            case 9:
                audioSource.clip = starsAndGalaxy;
                break;
            default:
                break;
        }
        audioSource.Play();
    }

    public void PlaySound(AudioClip soundEffect, float volume)
    {
        foreach(AudioSource source in soundSources)
        {
            // Loopataan soundSource taulukon läpi, onko joku audio source käyttämättä
            // Jos on, käytetään sitä ja soitetaan ääni efekti
            // Mikäli joitain ääniefektejä jää soittamatta, nosta taulukon kokoa
            if(!source.isPlaying)
            {
                source.clip = soundEffect;
                source.volume = volume;
                source.Play();
                break;
            }
        }
    }

    public void AddPoints()
    {
        /* Pisteet kasvavat aika kertoimella. Mitä nopeammin pelaaja tuhoaa laatikon, sitä enemmän pisteitä.
         * Pisteet per laatikko alkaa 10:stä ja laskee yhdellä joka secondsUntilDecrease intervallin välein yhdellä.
         */
        if ((15 - Math.Floor(Time.timeSinceLevelLoad / secondsUntilDecrease)) >= 1)
        {
            points += (int)(15 - Math.Floor(Time.timeSinceLevelLoad / secondsUntilDecrease));
        }
        else
        {
            points++;
        }
    }

    [System.Serializable]
    public class Leaderboard
    {
        public List<HighScoreEntry> list = new List<HighScoreEntry>();
    }

    public void Save(List<HighScoreEntry> scoresToSave)
    {
        leaderboard.list = scoresToSave;
        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Create);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }

    public List<HighScoreEntry> Load()
    {
        if (File.Exists(Application.persistentDataPath + "/HighScores/highscores.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Open);
            leaderboard = serializer.Deserialize(stream) as Leaderboard;
            stream.Close();
        }
        return leaderboard.list;
    }
}