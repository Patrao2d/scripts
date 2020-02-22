using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerName : MonoBehaviour
{
    // Texts
    public Text inputFieldText;

    // String
    [HideInInspector]public string playerName;

    // Gameobjects
    public GameObject errorNameGO;

    private static PlayerName _instance;

    public static PlayerName instance
    {
        get { return _instance; }
    }
    void Start()
    {
        _instance = this;
    }


    public void LoadNextScene()
    {
        playerName = inputFieldText.text;
        if (string.IsNullOrEmpty(playerName))
        {
            errorNameGO.SetActive(true);
            Debug.Log(playerName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
}
