using System;
using Unity.VisualScripting;
using UnityEngine;

// I, Andres Rosas Ortiz, 000800390 certify that this material is my original work. No other person's work has been used without due acknowledgement.

/// <summary>
/// Script that handles the movement and positioning of Platform objects.
/// </summary>
public class platformScript : MonoBehaviour
{
    Vector3 boundary;
    Vector2 platformSize;

    // main script reference
    mainLoop mainScript;
    GameObject Ground;

    /// <summary>
    /// Initializes variables at game start.
    /// </summary>
    void Start()
    {
        mainScript = GameObject.Find("Main Camera").GetComponent<mainLoop>();
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.z));
        platformSize = new Vector2(GetComponent<SpriteRenderer>().bounds.size.x, GetComponent<SpriteRenderer>().bounds.size.y);
        Ground = GameObject.Find("ground");
    }

    // Moves platforms and updates their positions.
    void Update()
    {
        // checks that the game has officially started - moves platforms down
        if (mainScript.startGame)
        {
            transform.Translate(new Vector2(0, -0.03f));
        }
        // checks if platform has fallen below screen view - randomly repositions it above screen view
        if (transform.position.y < -boundary.y - GetComponent<SpriteRenderer>().bounds.size.y / 2)
        {
            float newX;

            // checks which side of screen player is on to set new platform X to the other half of the screen
            if (mainScript.Player.transform.position.x < 0)
                newX = UnityEngine.Random.Range(0, boundary.x);
            else
                newX = UnityEngine.Random.Range(-boundary.x, 0);

            // repositions above screen view
            transform.position = new Vector2(newX, (boundary.y + platformSize.y / 2) * 2);
        }
    }
}
