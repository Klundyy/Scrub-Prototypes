using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Canvas MainMenu;
    public Canvas ControlsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenControls() {
        ControlsScreen.enabled = true;
        MainMenu.enabled = false;
    }

    public void CloseControls() {
        MainMenu.enabled = true;
        ControlsScreen.enabled = false;
    }
}
