using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Texts
    public TextMeshProUGUI timerText;

    // Chars
    private char splitter = ':';

    // Floats
    public float timer;

    // Bools
    private bool _active;

    private static Timer _instance;

    public static Timer instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        _instance = this;
        _active = true;
    }

    private void Update()
    {
        if (_active)
        {
            timer += Time.deltaTime;
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        float __seconds = (timer % 60);
        float __minutes = ((int)timer / 60) % 60;
        timerText.text = __minutes.ToString("00") + splitter + __seconds.ToString("00");
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public void StopTimer()
    {
        _active = !_active;
    }
}
