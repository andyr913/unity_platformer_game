using UnityEngine;

// I, Andres Rosas Ortiz, 000800390 certify that this material is my original work. No other person's work has been used without due acknowledgement.

/// <summary>
/// Script that handles when the Player object collides with a rigid object.
/// </summary>
public class playerCollisions : MonoBehaviour
{
    Vector3 boundary;
    // main script reference
    mainLoop mainScript;

    /// <summary>
    /// Initializes variables at game start.
    /// </summary>
    void Start()
    {
        mainScript = GameObject.Find("Main Camera").GetComponent<mainLoop>();
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.z));
    }

    /// <summary>
    /// Resets jumps and disables jumping animation when player collides with object.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        mainScript.Player.GetComponent<Animator>().SetBool("playerJumping", false);
        mainScript.twoJumps = false;
        mainScript.jumpCount = 0;
    }


}
