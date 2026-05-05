using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [Header("Settings")]
    public int bonusScore = 5;          // Очки за попадание
    public Color hitColor = Color.green; // Цвет при попадании
    public float flashDuration = 0.2f;   // Время вспышки перед перемещением

    [Header("Random Spawn Bounds (относительно центра стены)")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = 1f;
    public float maxY = 4.5f;

    private Renderer rend;
    private Color originalColor;
    private bool isBusy = false; // Защита от двойных срабатываний

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем: это мяч + мишень свободна + не в состоянии перемещения
        if (other.CompareTag("Ball") && !isBusy)
        {
            isBusy = true;

            // Начисляем очки
            if (GameManager.Instance != null)
                GameManager.Instance.AddPoints(bonusScore);

            // Визуальная вспышка
            if (rend != null)
                rend.material.color = hitColor;

            // Перемещаем на новую позицию через короткую паузу
            Invoke(nameof(Reposition), flashDuration);
        }
    }

    void Reposition()
    {
        // Генерируем случайную позицию в заданных границах
        float newX = Random.Range(minX, maxX);
        float newY = Random.Range(minY, maxY);
        
        // Z оставляем прежним (чуть впереди стены)
        transform.position = new Vector3(newX, newY, transform.position.z);

        // Возвращаем цвет и разблокируем мишень
        if (rend != null)
            rend.material.color = originalColor;

        isBusy = false;
    }
}