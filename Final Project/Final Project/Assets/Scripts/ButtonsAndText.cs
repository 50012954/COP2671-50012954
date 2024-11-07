using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAndText : MonoBehaviour
{
    // UI Elements
    public Text scoreText;
    public Text gameOverText;
    public Text titleText;
    public Text winText;
    public Button restartButton;
    public Button resumeButton;

    private PlayerController playerController;

    void Start()
    {
        // Reference the PlayerController to access its score and game state
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // Initially show the game title UI and hide other texts/buttons
        ShowTitleUI();

        // Add listeners to the buttons
        restartButton.onClick.AddListener(RestartGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    void Update()
    {

    }

    public void ShowTitleUI()
    {
        // Show title and hide other UI elements
        titleText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        // Show Game Over text and buttons
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);  // Resume button is not needed during game over
    }

    public void ShowWinUI()
    {
        // Show Win text and buttons
        winText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);  // Resume button is not needed when the game is won
    }

    public void ShowResumeUI()
    {
        // Show Resume button when game is paused
        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);  // No restart button when game is paused
        scoreText.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        // Call the RestartGame method from PlayerController
        playerController.RestartGame();
    }

    private void ResumeGame()
    {
        // Resume the game by unpausing and hiding the resume button
        playerController.gameOver = false;
        Time.timeScale = 1;  // Unpause the game
        ShowResumeUI();
    }
}
