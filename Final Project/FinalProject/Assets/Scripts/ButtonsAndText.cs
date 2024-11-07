using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonsAndText : MonoBehaviour
{
    public Button pauseButton, resumeButton, restartButton;
    public TextMeshProUGUI scoreText, gameOverText, titleText, winText;

    private bool isPaused = false;
    private PlayerController playerControllerScript;
    private float countdownTime = 60f;

    void Start()
    {
        // Set up button actions and initial UI state
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);

        // Configure initial button and text visibility
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // Update UI during gameplay
        if (!playerControllerScript.gameOver && !isPaused)
        {
            titleText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(true);

            // Update and display countdown timer
            countdownTime -= Time.deltaTime;
            scoreText.text = "Time: " + Mathf.Max(0, Mathf.Ceil(countdownTime)).ToString();

            if (countdownTime <= 0)
            {
                EndGame(); // Handle game over when timer reaches zero
            }
        }

        if (playerControllerScript.gameOver)
        {
            ShowGameOverUI(); // Show game over UI if the game is over
        }
    }
    public void RestartGame()
    {
        // Restart the current scene
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        // Reset gravity to default before going back to the main menu
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        // Pause the game and update UI elements
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseButton.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        // Resume the game from paused state
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseButton.gameObject.SetActive(true);
            resumeButton.gameObject.SetActive(false);
        }
    }
    private void EndGame()
    {
        // Set game over state and update UI
        playerControllerScript.gameOver = true;
        scoreText.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
        ShowGameOverUI();
    }

    public void ShowGameOverUI()
    {
        // Show game over related UI elements
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
    }

    public void ShowTitleUI()
    {
        // Display the game title
        titleText.gameObject.SetActive(true);
    }
}
