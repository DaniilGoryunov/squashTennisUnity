using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;
    public GameManager gameManager;

    private Vector3 initialPos;
    private bool hasHitTarget = false;
    private float lastHitTime = 0f; // Для отслеживания активности мяча

    private void Start()
    {
        initialPos = transform.position;
        if (spawnPoint == null) spawnPoint = transform;
    }

    // Вызывается из Player.cs при ударе, чтобы зафиксировать время
    public void MarkLastHitTime() => lastHitTime = Time.time;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<TargetCircle>(out var target))
        {
            hasHitTarget = true;
        }

        // Отскок от стены
        if (collision.transform.CompareTag("Wall"))
        {
            if (!hasHitTarget && gameManager != null)
                gameManager.AddPoints(1);
            
            // Не сбрасываем сразу! Ждём, пока мяч долетит обратно к игроку или упадёт
            return;
        }

        // Упал под пол
        if (transform.position.y < -0.5f)
        {
            ResetBall();
        }
    }

    void Update()
    {
        // Если мяч улетел за спину игрока
        if (transform.position.z > 4f)
            ResetBall();

        // Если мяч стоит на месте больше 3 секунд после удара
        if (Time.time > lastHitTime + 3f && GetComponent<Rigidbody>().linearVelocity.magnitude < 0.1f)
            ResetBall();
    }

    void ResetBall()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        transform.position = spawnPoint.position;
        hasHitTarget = false;
    }
}