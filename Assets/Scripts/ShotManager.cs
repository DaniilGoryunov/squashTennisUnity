using UnityEngine;

[System.Serializable]
public class Shot
{
    public float hitForce = 22f; // Горизонтальная скорость (м/с)
    public float upForce = 2f;   // Вертикальная подброска
}

public class ShotManager : MonoBehaviour
{
    [Header("Shot Types")]
    public Shot topSpin; // Топспин: стабильный, чуть выше
    public Shot flat;    // Плоский: быстрый, низкий

    // Значения по умолчанию, чтобы не настраивать вручную
    private void Reset()
    {
        topSpin.hitForce = 22f; topSpin.upForce = 3f;
        flat.hitForce = 28f;    flat.upForce = 1.5f;
    }
}