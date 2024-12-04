using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CheckWinCon : MonoBehaviour
{
    public float timeRemaining = 60f; // Set time limit to 60 seconds
    private bool timerRunning = true;
    public TextMeshProUGUI timerText;

    private WindowScript[] windows;
    // Start is called before the first frame update
    void Start()
    {
        windows = FindObjectsOfType<WindowScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        if (timerRunning)
        {
            // Decrease the time
            timeRemaining -= Time.deltaTime;

            // Update the displayed text
            if (timeRemaining > 0)
            {
                timerText.text = $"Time Remaining: {Mathf.Ceil(timeRemaining)}s";
            }
            else
            {
                loseState();
            }
        }

        bool gameWon = true;
        foreach (WindowScript win in windows) {
            if(win.alpha < 1f) {
                gameWon = false;
            }
        }
        if(gameWon) {
            winState();
        }
    }

    void winState() {
        Debug.Log("WIN!");
        SceneManager.LoadScene("WinScene");
    }

    void loseState()
    {
        Debug.Log("LOSE!");
        SceneManager.LoadScene("LoseScene"); // Ensure you have a "LoseScene" set up
    }


}
