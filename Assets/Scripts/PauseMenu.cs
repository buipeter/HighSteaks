using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    private bool checkRestartButton;

    // on game start, does not show the pauseMenu
    void Start()
    {
        checkRestartButton = false;
        pauseMenu.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        // checks if player presses ESCAPE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if pressed ESCAPE, game will check if the game is paused already
            if (isPaused)
            {
                // if it is paused already, then call ResumeGame, like flipping a light switch
                ResumeGame();
            }
            else
            {
                // if isn't then call PauseGame
                PauseGame();
            }
        }
        // updates and checks if the restart button was pressed, if pressed it will reset every collectible,
        // and time, and get the game back into its normal state
        if (checkRestartButton)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            CollectiblePickup.total = 0;
            isPaused = false;
            GameManager.currentCollectibles = 0;
        }
        
    }

    public void PauseGame()
    {
        // checks if the level is not completed, so it does not cause bugs
        // there was a bug when if level is completed and you press escape, you will still get the pause menu
        // and you can press escape again and it will activate the camera controls.
        if (!GameManager.isLevelComplete)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Resumes the game at that players current position
    // the timer will continue, and the mouse will be locked again
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // a button within our PauseMenu to restart the level
    public void RestartLevel()
    {
        checkRestartButton = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // a button within our PauseMenu to exit the program completely
    public void QuitGame()
    {
        Application.Quit();
    }

    // a button within our PauseMenu to return to hub
    public void ReturnToHub()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Hub");
    }
}
