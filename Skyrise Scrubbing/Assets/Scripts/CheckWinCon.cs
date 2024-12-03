using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWinCon : MonoBehaviour
{
    private WindowScript[] windows;
    // Start is called before the first frame update
    void Start()
    {
        windows = FindObjectsOfType<WindowScript>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
