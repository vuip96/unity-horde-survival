
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SurvivalTimer : MonoBehaviour
{
    private TMP_Text timerText;
    private float elapsedTime;

    private void Awake()
    {
        // Get the TextMeshPro component
        timerText = GetComponent<TMP_Text>();

        // Reset the survival timer
        elapsedTime = 0f;
        UpdateTimerText();
    }

    private void Update()
    {
        // Count elapsed gameplay time
        elapsedTime += Time.deltaTime;

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        // Convert elapsed seconds to minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Display time in MM:SS format
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
