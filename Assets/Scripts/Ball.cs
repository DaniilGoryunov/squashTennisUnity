using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;
    public GameManager gameManager;

    private Rigidbody rb;
    private bool hasHitTarget = false;
    private float lastHitTime = 0f; // Чтобы мяч не сбрасывался сразу после удара

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (spawnPoint == null) spawnPoint = transform;
        lastHitTime = Time.time;
    }

    // Вызывается из Player.cs при успешном ударе
    public void MarkLastHitTime() => lastHitTime = Time.time;

    void Update()
    {
        if (rb == null) return;

        // 1. Мяч улетел за спину игрока (Z > 3)
        if (transform.position.z > 3f) OnMiss();
        // 2. Мяч провалился под пол
        else if (transform.position.y < -0.5f) OnMiss();
        // 3. Мяч остановился на полу > 3 секунд
        else if (Time.time > lastHitTime + 3f && rb.linearVelocity.magnitude < 0.2f) OnMiss();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Попал в мишень
        if (collision.gameObject.TryGetComponent<TargetCircle>(out _))
            hasHitTarget = true;

        // Попал в стену
        if (collision.transform.CompareTag("Wall"))
        {
            if (!hasHitTarget && gameManager != null)
                gameManager.AddPoints(1);
            // Мяч отскакивает, ждём, пока он вернётся к игроку
        }
    }

    void OnMiss()
    {
        if (gameManager != null)
            gameManager.ResetScore(); // ← Обнуляем счёт при промахе

        ResetBall();
    }

    void ResetBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnPoint.position;
        hasHitTarget = false;
        lastHitTime = Time.time; // Сбрасываем таймер, чтобы Update не сработал дважды
    }
}