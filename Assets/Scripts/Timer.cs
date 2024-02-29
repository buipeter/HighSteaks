using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer;

    private void Start()
    {
        // Initialize the timer to zero
        timer = 0f;
    }

    private void Update()
    {
        // Update the timer every frame
        timer += Time.deltaTime;

        // Format the timer as minutes:seconds
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00.00");

        // Update the TextMeshPro Text component
        timerText.text = $"{minutes}:{seconds}";
    }
}
