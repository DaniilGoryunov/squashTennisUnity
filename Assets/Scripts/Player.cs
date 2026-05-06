using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Transform ball;
    public Animator animator;
    public ShotManager shotManager;

    [Header("Settings")]
    public float moveSpeed = 4f;
    public float hitRange = 2.5f;

    private Shot currentShot;

    void Start()
    {
        animator ??= GetComponent<Animator>();
        shotManager ??= GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        float h = 0f, v = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h = 1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) v = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) v = -1f;

        Vector3 move = (transform.right * h + transform.forward * v) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -12f, 12f), 0f, transform.position.z);

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            currentShot = shotManager.topSpin;
            TryHit();
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentShot = shotManager.flat;
            TryHit();
        }
    }

    void TryHit()
    {
        if (ball == null) return;
        float dist = Vector3.Distance(transform.position, ball.position);
        if (dist < hitRange)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
        
                Vector3 forward = transform.forward;
                forward.y = 0;
                forward.Normalize();

                rb.linearVelocity = forward * currentShot.hitForce + Vector3.up * currentShot.upForce;

                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript != null) ballScript.MarkLastHitTime();

                if (animator != null)
                {
                    Vector3 ballDir = ball.position - transform.position;
                    animator.Play(ballDir.x >= 0 ? "forehand" : "backhand", 0, 0f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) TryHit();
    }
}