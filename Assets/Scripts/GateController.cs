using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{

    public string level1SceneName = "Level1";
    public string level2SceneName = "Level2";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Level1"))
            {
                SceneManager.LoadScene(level1SceneName);
                CollectiblePickup.total = 0;
            }
            else if (gameObject.CompareTag("Level2") && GameManager.level1Completed)
            {
                SceneManager.LoadScene(level2SceneName);
            }
        }
    }
}
