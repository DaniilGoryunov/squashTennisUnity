using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Wall Milestone")]
    public Renderer wallRenderer;      // Перетащи объект Wall
    public Material wallMaterial10;    // Перетащи новый материал
    public Material wallMaterialOriginal;

    private int currentScore = 0;
    private bool wallMaterialChanged = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        UpdateUI();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        Debug.Log($"[GameManager] Счёт: {currentScore} (добавлено +{points})");
        UpdateUI();
        CheckMilestones();
    }

    void CheckMilestones()
    {
        if (!wallMaterialChanged && currentScore >= 10)
        {
            if (wallRenderer == null)
            {
                Debug.LogWarning("[GameManager] Поле Wall Renderer пустое!");
                return;
            }
            if (wallMaterial10 == null)
            {
                Debug.LogWarning("[GameManager] Поле Wall Material 10 пустое!");
                return;
            }

            Debug.Log("[GameManager] 🎯 ДОСТИГНУТО 10 ОЧКОВ! Меняю материал стены.");
            wallRenderer.material = wallMaterial10;
            wallMaterialChanged = true;
        }
    }

    public void ResetScore()
    {
        Debug.Log($"[GameManager] Сброс счёта. Было: {currentScore} -> Стало: 0");
        currentScore = 0;
        UpdateUI();

        if (wallRenderer != null && wallMaterialOriginal != null) wallRenderer.material = wallMaterialOriginal;
        wallMaterialChanged = false;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Счёт: " + currentScore;
    }
}