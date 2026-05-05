using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText; // Перетащи сюда ScoreText

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        UpdateUI(); // Показать "Счёт: 0" сразу
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateUI();
    }

    // Вызывается при промахе мяча
    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Счёт: " + currentScore;
    }
}