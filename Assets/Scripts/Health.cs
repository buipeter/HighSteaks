using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        healthText.gameObject.SetActive(true);
        //playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"Health: {playerController.health}/{playerController.maxHealth}";
    }
}
