using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int currentCollectibles;
    public TextMeshProUGUI collectibleText;
    public GameObject EndGameMenu;

    // bool to check if the level is completed
    public static bool isLevelComplete;
    public static bool level1Completed;


    void Start()
    {
        collectibleText.text = "Steaks: " + currentCollectibles.ToString() + " / " + CollectiblePickup.total;
        isLevelComplete = false;
        level1Completed = false;
        EndGameMenu.SetActive(false);
        currentCollectibles = 0;
    }

    private void Update()
    {
        // check if the player collects all collectibles and if isLevelComplete is false
        if (currentCollectibles == CollectiblePickup.total && !isLevelComplete)
        {
            LevelComplete();
        }

        // if isLevelComplete is true, then give player option to press space to reset and play again
        if (isLevelComplete)
        {
            //CollectiblePickup.total = 0;
            //currentCollectibles = 0;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            EndGameMenu.SetActive(true);
        }
    }

    public void AddCollectibles(int collectiblesToAdd)
    {
        // counter for collectibles to track.
        // will be added on trigger in CollectiblePickup script
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
            // if exists, update the text out of total collectibles on map
            collectibleText.text = "Steaks: " + currentCollectibles.ToString() + " / " + CollectiblePickup.total;
        }
        else
        {
            Debug.LogWarning("Text UI does not exist!");
        }
    }

    // once all collectibles is collected, level complete is called
    public void LevelComplete()
    {
        // stops the game time, and shows the winText
        Time.timeScale = 0f;

        // sets the isLevelComplete to true as the level is completed
        isLevelComplete = true;
        level1Completed = true;
    }

    // resets everything back to default
    private void Reset()
    {
        CollectiblePickup.total = 0;
        currentCollectibles = 0;
        Time.timeScale = 1f;
        isLevelComplete = false;
    }
}
