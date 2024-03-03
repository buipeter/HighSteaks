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
    public static bool isLevelComplete;
    // Start is called before the first frame update
    void Start()
    {
        winText.enabled = false;
        isLevelComplete = false;
    }

    private void Update()
    {
        if (currentCollectibles == 8 && !isLevelComplete)
        {
            LevelComplete();
        }

        if (isLevelComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Reset();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void AddCollectibles(int collectiblesToAdd)
    {   
        currentCollectibles += collectiblesToAdd;

        //update the call to collectible text ui
        UpdateCollectibleText();
    }
    public void UpdateCollectibleText()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "Steaks: " + currentCollectibles.ToString() + " / 8";
        }
        else
        {
            Debug.LogWarning("Collectible does not exist!");
        }
    }
    public void LevelComplete()
    {
        Time.timeScale = 0f;
        winText.enabled = true;
        winText.text = "You win! Thank you for playing Checkpoint 1! Press Space to play again!";

        isLevelComplete = true;
    }

    private void Reset()
    {
        currentCollectibles = 0;
        Time.timeScale = 1f;
        winText.enabled = false;
        isLevelComplete = false;
    }
}
