using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private float score = 0f;
    private ButtonsAndText buttonsAndText;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public GameObject powerupIndicator;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    public bool hasPowerup;

    private float powerupIndicatorOffsetY = 1.5f;

    void Start()
    {
        // Initialize references to Rigidbody, Animator, and AudioSource components
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Reference UI elements and show the game title at start
        buttonsAndText = GameObject.Find("GameManager").GetComponent<ButtonsAndText>();
        buttonsAndText.ShowTitleUI();

        powerupIndicator.SetActive(false); // Initially hide powerup indicator
    }

    void Update()
    {
        // Only allow actions when game is active
        if (!gameOver)
        {
            // Check for jump input if the player is on the ground
            if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
            {
                Jump();
            }
            // Apply downward force if down arrow is pressed
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerRb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
            }

            score += Time.deltaTime;

            // Update powerup indicator position to follow the player
            if (hasPowerup)
            {
                powerupIndicator.transform.position = transform.position + new Vector3(0, powerupIndicatorOffsetY, 0);
            }
        }
    }

    private void Jump()
    {
        // Apply upward force to simulate jump
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if player collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        // If player hits an obstacle, check for powerup
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (hasPowerup)
            {
                Destroy(collision.gameObject);
                hasPowerup = false;
                powerupIndicator.SetActive(false);
            }
            else
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        // Set game state to over and play animations and effects for death
        gameOver = true;
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        explosionParticle.Play();
        dirtParticle.Stop();
        playerAudio.PlayOneShot(crashSound, 1.0f);
        buttonsAndText.ShowGameOverUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    public void RestartGame()
    {
        Physics.gravity /= gravityModifier;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}