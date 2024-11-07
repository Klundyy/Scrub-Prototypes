using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseScrubbing : MonoBehaviour
{
    public float scrubPower = 0f;
    public float scrubIncreaseRate = 1f;
    public float scrubDecreaseRate = 0.5f;
    public float maxScrubPower = 100f;
    public float minScrubPower = 0f;

    private bool isScrubbing = false;
    private ScrubManager scrubManager;

    void Start()
    {
        scrubManager = FindObjectOfType<ScrubManager>(); // Get ScrubManager reference
    }

    void Update()
    {
        if (isScrubbing)
        {
            scrubPower += scrubIncreaseRate * Time.deltaTime ;
        }
        else
        {
            scrubPower -= scrubDecreaseRate * Time.deltaTime ;
        }

        // Clamp scrub power
        scrubPower = Mathf.Clamp(scrubPower, minScrubPower, maxScrubPower);

        // Update the scrub power in ScrubManager
        if (scrubManager != null)
        {
            scrubManager.scrubPower = scrubPower;
        }

       // Debug.Log($"Scrub Power: {scrubPower}");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            isScrubbing = true;
        }
        else
        {
            isScrubbing = false;
        }
    }

    void OnMouseExit()
    {
        isScrubbing = false;
    }
    public void ResetScrubPower()
    {
        scrubPower = 0f;
        if (scrubManager != null)
        {
            scrubManager.scrubPower = 0f;
        }
    }
}
