using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int currentCollectibles;
    public TextMeshProUGUI collectibleText;
    public TextMeshProUGUI winText;
    // bool to check if the level is completed
    public static bool isLevelComplete;
    // Start is called before the first frame update
    void Start()
    {
        winText.enabled = false;
        isLevelComplete = false;
    }

    private void Update()
    {
        // if the player collects 8 or if isLevelComplete is false
        if (currentCollectibles == 8 && !isLevelComplete)
        {
            LevelComplete();
        }

        // if isLevelComplete is true, then give player option to press space to reset and play again
        if (isLevelComplete)
        {
            // if player press space, it will call for a reset in the collectibles and the scene will reload
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Reset();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void AddCollectibles(int collectiblesToAdd)
    {
        // counter for collectibles to track.
        currentCollectibles += collectiblesToAdd;

        //update the call to collectible text ui everytime player collects collectible
        UpdateCollectibleText();
    }

    // every time a player collects a collectible, update the text
    public void UpdateCollectibleText()
    {
        // check if it exists
        if (collectibleText != null)
        {
            // if exists, update the text out of 8 (max collectibles on checkpoint 1)
            collectibleText.text = "Steaks: " + currentCollectibles.ToString() + " / 8";
        }
        else
        {
            Debug.LogWarning("Collectible does not exist!");
        }
    }

    // once all 8 collectibles is completed, level complete is called
    public void LevelComplete()
    {
        // stops the game time, and shows the winText
        Time.timeScale = 0f;
        winText.enabled = true;
        winText.text = "You win! Thank you for playing Checkpoint 1! Press Space to play again!";

        // sets the isLevelComplete to true as the level is completed
        isLevelComplete = true;
    }

    // resets everything back to default
    private void Reset()
    {
        currentCollectibles = 0;
        Time.timeScale = 1f;
        winText.enabled = false;
        isLevelComplete = false;
    }
}
