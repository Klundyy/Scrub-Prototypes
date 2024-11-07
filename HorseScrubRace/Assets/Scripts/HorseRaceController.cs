using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseRaceController : MonoBehaviour
{
    public float baseSpeed = 2f;
    public float playerSpeedMultiplier = 0.1f;
    public bool isPlayer;
    public Transform finishLine;
    public float finishOffset = 0.1f;

    private ScrubManager scrubManager; // Reference to ScrubManager
    private bool raceFinished = false;

    void Start()
    {
        if (isPlayer)
        {
            scrubManager = FindObjectOfType<ScrubManager>(); // Get ScrubManager reference
            if (scrubManager == null)
            {
                Debug.LogError("ScrubManager not found!");
            }
        }
    }

    void Update()
    {
        if (raceFinished) return;

        float currentSpeed = baseSpeed;

        if (isPlayer && scrubManager != null)
        {
            currentSpeed += scrubManager.scrubPower * playerSpeedMultiplier;
        }

        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);

        CheckFinishLine();
    }

    void CheckFinishLine()
    {
        if (Mathf.Abs(transform.position.y - finishLine.position.y) <= finishOffset)
        {
            raceFinished = true;
            Debug.Log(isPlayer ? "Player Wins!" : "Computer Wins!");
            FindObjectOfType<RaceManager>().DeclareWinner(isPlayer);
        }
    }

    // New ResetRace method
    public void ResetRace()
    {
        raceFinished = false; // Reset the raceFinished state
    }
}
