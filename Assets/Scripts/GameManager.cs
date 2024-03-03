using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentCollectibles;
    public TextMeshProUGUI collectibleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            collectibleText.text = "Steaks: " + currentCollectibles.ToString();
        }
        else
        {
            Debug.LogWarning("Collectible does not exist!");
        }
    }
}
