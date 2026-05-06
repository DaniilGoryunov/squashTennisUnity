using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [Header("Settings")]
    public int bonusScore = 5;
    public Color hitColor = Color.green;
    public float flashDuration = 0.2f;

    [Header("Random Spawn Bounds (относительно центра стены)")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = 1f;
    public float maxY = 4.5f;

    private Renderer rend;
    private Color originalColor;
    private bool isBusy = false; 

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !isBusy)
        {
            isBusy = true;

            if (GameManager.Instance != null)
                GameManager.Instance.AddPoints(bonusScore);

            if (rend != null)
                rend.material.color = hitColor;

            Invoke(nameof(Reposition), flashDuration);
        }
    }

    void Reposition()
    {
        float newX = Random.Range(minX, maxX);
        float newY = Random.Range(minY, maxY);
        
        transform.position = new Vector3(newX, newY, transform.position.z);

        if (rend != null)
            rend.material.color = originalColor;

        isBusy = false;
    }
}