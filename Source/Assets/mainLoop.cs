using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// I, Andres Rosas Ortiz, 000800390 certify that this material is my original work. No other person's work has been used without due acknowledgement.

/// <summary>
/// Main script that controls the flow of a platform game. Player can move and jump on platforms.
/// </summary>
public class mainLoop : MonoBehaviour
{
    // declaring game objects and variables
    public GameObject Player;
    public GameObject Ground;
    public GameObject Platform;

    GameObject instructions;

    public GameObject mainTheme;

    AudioSource mainThemeSource;

    public float playerSpeed;
    public float jumpHeight;
    public int jumpCount = 0;

    public bool startGame;
    public bool twoJumps = false;

    Vector3 boundary;
    Vector2 playerSize;
    Vector2 platformSize;

    /// <summary>
    /// Initializes game variables and starts game music at game start.
    /// </summary>
    void Start()
    {
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.z));
        playerSize = new Vector2(Player.GetComponent<SpriteRenderer>().bounds.size.x, Player.GetComponent<SpriteRenderer>().bounds.size.y);
        platformSize = new Vector2(Platform.GetComponent<SpriteRenderer>().bounds.size.x, Platform.GetComponent<SpriteRenderer>().bounds.size.y);
        instructions = GameObject.Find("Instructions");
        mainThemeSource = mainTheme.GetComponent<AudioSource>();

        mainThemeSource.loop = true;
        mainThemeSource.Play();
    }

    /// <summary>
    /// Updates the state of the game every frame.
    /// </summary>
    void Update()
    {
        // left arrow movement
        if (Input.GetKey(KeyCode.LeftArrow) && Player.transform.position.x > -boundary.x + playerSize.x / 2)
        {
            movePlayer(new Vector2(-1, 0));
            // flips player sprite direction
            Player.GetComponent<SpriteRenderer>().flipX = true;

            // enables player running parameter for running animation - if player is not jumping
            if (!Player.GetComponent<Animator>().GetBool("playerJumping"))
                Player.GetComponent<Animator>().SetBool("playerRunning", true);
        }
        // right arrow movement
        else if (Input.GetKey(KeyCode.RightArrow) && Player.transform.position.x < boundary.x - playerSize.x / 2)
        {
            movePlayer(new Vector2(1, 0));
            Player.GetComponent<SpriteRenderer>().flipX = false;

            // enables player running parameter for running animation
            if (!Player.GetComponent<Animator>().GetBool("playerJumping"))
                Player.GetComponent<Animator>().SetBool("playerRunning", true);
        } else
        {
            // disables player running parameter - to play idle animation instead
            Player.GetComponent<Animator>().SetBool("playerRunning", false);
        }
        // double jump movement
        if (Input.GetKeyDown(KeyCode.Space) && !twoJumps)
        {
            playerJump();
            jumpCount++;
        }
        // moves ground below screen when game starts
        if (startGame)
        {
            Ground.transform.Translate(new Vector2(0, -0.03f));
            instructions.SetActive(false);
        }
        // ends game if player is below screen - stops theme and plays game over sound
        if (Player.transform.position.y < - boundary.y - playerSize.y / 2)
        {
            mainThemeSource.Stop();
            mainThemeSource.loop = false;

            // updates text on screen
            instructions.GetComponent<TextMeshPro>().text = "Game Over!";
            instructions.SetActive(true);

            startGame = false;
            return;
        }
    }

    /// <summary>
    /// Moves the player by translating its position.
    /// </summary>
    /// <param name="direction">The Vector to move by</param>
    void movePlayer(Vector2 direction)
    {
        Player.transform.Translate(direction * playerSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Makes the player jump.
    /// </summary>
    void playerJump()
    {
        // starts game when player jumps for first time
        if (!startGame)
            startGame = true;

        // adds physics to player and jumps
        Rigidbody2D rbProperty = Player.GetComponent<Rigidbody2D>();
        rbProperty.AddForce(new Vector2(rbProperty.linearVelocity.x, jumpHeight));

        // enables player jumping animation
        Player.GetComponent<Animator>().SetBool("playerJumping", true);
        // disables player running animation
        Player.GetComponent<Animator>().SetBool("playerRunning", false);
        // plays jump sound
        Player.GetComponent<AudioSource>().Play();

        // counts for 2 jumps to enable double jump
        if (jumpCount == 1)
            twoJumps = true;
    }
}
