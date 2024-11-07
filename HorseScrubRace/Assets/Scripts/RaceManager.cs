using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    private bool winnerDeclared = false;
    public GameObject playerWinsText; // Reference to the PlayerWins UI Text
    public GameObject computerWinsText; // Reference to the ComputerWins UI Text

    public Transform playerHorse; // Reference to PlayerHorse
    public Transform computerHorse; // Reference to ComputerHorse
    public Transform playerStartLine; // Player's starting position
    public Transform computerStartLine;


    public void DeclareWinner(bool isPlayer)
    {
        if (winnerDeclared) return; // Ensure only one winner is declared
        winnerDeclared = true;

        if (isPlayer)
        {
            Debug.Log("Player Wins!");
            playerWinsText.SetActive(true); // Enable PlayerWins text
        }
        else
        {
            Debug.Log("Computer Wins!");
            computerWinsText.SetActive(true); // Enable ComputerWins text
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetRace();
        }
    }

    public void ResetRace()
    {
        // Reset UI
        playerWinsText.SetActive(false);
        computerWinsText.SetActive(false);

        // Reset winner state
        winnerDeclared = false;

        // Reset positions of both horses
        playerHorse.position = playerStartLine.position;
        computerHorse.position = computerStartLine.position;

        // Reset horse race states
        playerHorse.GetComponent<HorseRaceController>().ResetRace();
        computerHorse.GetComponent<HorseRaceController>().ResetRace();

        var horseScrubbing = FindObjectOfType<HorseScrubbing>();
        if (horseScrubbing != null)
        {
            horseScrubbing.ResetScrubPower(); // Reset scrub in HorseScrubbing too
        }



        Debug.Log("Race Reset!");
    }
}

