using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // Buttons
    public Button cardGameButton;
    public Button leaderboardButton;

    // GameObjects
    public GameObject playAgainGO;
    public GameObject cardGameGO;
    public GameObject leaderboardGO;

    // Bools
    private bool gameSelected;

    private static CanvasManager _instance;

    public static CanvasManager instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        _instance = this;
    }

    private void Update()
    {
        if (gameSelected)
        {
            cardGameButton.Select();
        }
        else
        {
            leaderboardButton.Select();
        }
    }

    public void PlayAgainButton()
    {
        GameManager.instance.ResetGame();
        playAgainGO.SetActive(false);
    }

    public void ActivePlayAgain()
    {
        playAgainGO.SetActive(true);
    }

    public void ChangeGameSelected()
    {
        gameSelected = !gameSelected;
    }

    public void ActiveLeaderboard()
    {
        if (gameSelected) return;
        leaderboardGO.SetActive(true);
        cardGameGO.SetActive(false);
        ChangeGameSelected();
    }

    public void ActiveGame()
    {
        if (!gameSelected) return;
        leaderboardGO.SetActive(false);
        cardGameGO.SetActive(true);
        ChangeGameSelected();
    }
   
}
