using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Transforms
    public Transform[] cardsTransform;
    public Transform[] lockedCardsTransform;

    // GameObjects
    public GameObject[] cardsPrefabs;

    // Lists
    public List<GameObject> flippedCardsList = new List<GameObject>();
    public List<GameObject> _drawCardsList = new List<GameObject>();
    

    // Ints
    private int _nFlippedCards = 0;
    private int _nDrawCards = 8;
    [HideInInspector] public int nOfClicks = 0;
    private int _currentMatches = 0;
    private int _tries = 0;
    public float score;

    // Texts
    public TextMeshProUGUI triesText;
    public Text playerText;


    private static GameManager _instance;

    public static GameManager instance
    {
        get { return _instance; }
    }

    void Start()
    {
        _instance = this;
        DrawBoard();
        playerText.text = PlayerName.instance.playerName;

    }

    private void Update()
    {
        score = Mathf.RoundToInt((_tries * 5) + Timer.instance.timer);
    }


    private void DrawBoard()
    {
        // Wich cards to draw
        for (int i = 0; i < _nDrawCards; i++)
        {
            int __randomCardToDraw = Random.Range(0, cardsPrefabs.Length);
            if (_drawCardsList.Contains(cardsPrefabs[__randomCardToDraw]))
            {
                i--;
            } else
            {
                _drawCardsList.Add(cardsPrefabs[__randomCardToDraw]);
            }
        }
        
        // Where to put them
        for (int i = 0; i < _drawCardsList.Count; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                int __randomPosition = Random.Range(0, cardsTransform.Length);
                if (cardsTransform[__randomPosition].childCount > 0)
                {
                    k--;
                } else
                {
                    Instantiate(_drawCardsList[i], cardsTransform[__randomPosition]);
                }
            }
        }
    }

    public void CompareCards()
    {
        if (nOfClicks <= 2) return;
        if (flippedCardsList[0].GetComponent<Card>().cardID == flippedCardsList[1].GetComponent<Card>().cardID && 
            flippedCardsList[1].GetComponent<Card>().cardID == flippedCardsList[2].GetComponent<Card>().cardID)
        {
            for (int i = 0; i < flippedCardsList.Count; i++)
            {
                flippedCardsList[i].GetComponent<Transform>().SetParent(lockedCardsTransform[0 + _currentMatches], false);
                flippedCardsList[i].GetComponent<Card>().YayComparation();
            }
            _currentMatches++;
            if (_currentMatches == 8)
            {
                _currentMatches = 0;
                EndGame();
            }
        }
        else
        {
            for (int i = 0; i < flippedCardsList.Count; i++)
            {
                flippedCardsList[i].GetComponent<Card>().FailComparation();
            }
        }
        flippedCardsList.Clear();
        _tries++;
        triesText.text = _tries.ToString("");
        nOfClicks = 0;
    }

    public void EndGame()
    {
        score = Mathf.RoundToInt((_tries * 5) + Timer.instance.timer);
        CanvasManager.instance.ActivePlayAgain();
        Timer.instance.StopTimer();
        HighscoreTable.instance.AddHighscoreEntry((int)score, PlayerName.instance.playerName, Timer.instance.timer);
    }

    public void ResetGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}
