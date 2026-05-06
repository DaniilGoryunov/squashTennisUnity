using UnityEngine;

[System.Serializable]
public class Shot
{
    public float hitForce = 22f;
    public float upForce = 2f; 
}

public class ShotManager : MonoBehaviour
{
    [Header("Shot Types")]
    public Shot topSpin;
    public Shot flat;  

    private void Reset()
    {
        topSpin.hitForce = 22f; topSpin.upForce = 3f;
        flat.hitForce = 28f;    flat.upForce = 1.5f;
    }
}