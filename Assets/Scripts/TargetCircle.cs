using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [Header("Settings")]
    public int bonusScore = 5;        // Сколько очков даёт попадание
    public Color hitColor = Color.green; // Цвет при попадании
    public float resetDelay = 1.5f;   // Время до восстановления

    private Renderer rend;
    private bool isHit = false;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем: это мяч + мишень ещё не активирована
        if (other.CompareTag("Ball") && !isHit)
        {
            isHit = true;
            
            // Меняем цвет
            if (rend != null)
                rend.material.color = hitColor;

            // Начисляем бонусные очки через менеджер
            if (GameManager.Instance != null)
                GameManager.Instance.AddPoints(bonusScore);

            // Восстанавливаем мишень через время
            Invoke(nameof(ResetTarget), resetDelay);
        }
    }

    void ResetTarget()
    {
        isHit = false;
        if (rend != null)
            rend.material.color = originalColor;
    }
}