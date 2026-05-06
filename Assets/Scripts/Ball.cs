using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;
    public GameManager gameManager;

    private Rigidbody rb;
    private bool hasHitTarget = false;
    private float lastHitTime = 0f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (spawnPoint == null) spawnPoint = transform;
        lastHitTime = Time.time;
    }

    public void MarkLastHitTime() => lastHitTime = Time.time;

    void Update()
    {
        if (rb == null) return;

        if (transform.position.z > 3f) OnMiss();
        else if (transform.position.y < -0.5f) OnMiss();
        else if (Time.time > lastHitTime + 3f && rb.linearVelocity.magnitude < 0.2f) OnMiss();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<TargetCircle>(out _))
            hasHitTarget = true;

        if (collision.transform.CompareTag("Wall"))
        {
            if (!hasHitTarget && gameManager != null)
                gameManager.AddPoints(1);
        }
    }

    void OnMiss()
    {
        if (gameManager != null)
            gameManager.ResetScore();

        ResetBall();
    }

    void ResetBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnPoint.position;
        hasHitTarget = false;
        lastHitTime = Time.time;
    }
}