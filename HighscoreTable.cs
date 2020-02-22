using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour 
{
    // Transforms
    public Transform entryContainer;
    public Transform entryTemplate;

    // Lists
    private List<Transform> highscoreEntryTransformList;

    private static HighscoreTable _instance;

    public static HighscoreTable instance
    {
        get { return _instance; }
    }


    private void Start()
    {
        _instance = this;
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;

        entryTransform.Find("PositionText").GetComponent<Text>().text = rank.ToString();

        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        float time = highscoreEntry.time;
        float seconds = (time % 60);
        float minutes = ((int)time / 60) % 60;
        char splitter = ':';
        entryTransform.Find("TimeText").GetComponent<Text>().text = minutes.ToString("00") + splitter + seconds.ToString("00");

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name, float time) {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name, time = time};
        
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public string name;
        public float time;
    }
    
}
