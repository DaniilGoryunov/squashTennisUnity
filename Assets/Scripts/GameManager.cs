using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Глобальная ссылка для доступа из других скриптов

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private int playerScore = 0;

    void Awake()
    {
        // Синглтон-паттерн: гарантируем, что экземпляр один
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не удалять при смене сцены (опционально)
        }
        else
        {
            Destroy(gameObject); // Если уже есть менеджер — удаляем дубль
        }
    }

    // Публичный метод для добавления очков (вызывается из TargetCircle и Ball)
    public void AddPoints(int points)
    {
        playerScore += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Счёт: " + playerScore;
        }
    }
}