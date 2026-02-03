using UnityEngine;

public class HeadMotionController : MonoBehaviour
{
    [Header("Activar efectos")]
    public bool breathing = true;
    public bool headBob = false;
    public bool microNoise = true;

    [Header("Respiración")]
    public float breathingSpeed = 1.2f;
    public float breathingAmount = 0.03f;

    [Header("Head Bob (caminar)")]
    public float bobSpeed = 7f;
    public float bobAmount = 0.05f;

    [Header("Micro Ruido")]
    public float noiseSpeed = 8f;
    public float noiseAmount = 0.8f;

    Vector3 startPos;
    float time;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        time += Time.deltaTime;

        Vector3 offset = Vector3.zero;

        // Respiración
        if (breathing)
        {
            offset.y += Mathf.Sin(time * breathingSpeed) * breathingAmount;
        }

        // Head bob
        if (headBob)
        {
            offset.y += Mathf.Sin(time * bobSpeed) * bobAmount;
        }

        transform.localPosition = startPos + offset;

        // Micro vibración
        if (microNoise)
        {
            float x = (Mathf.PerlinNoise(time * noiseSpeed, 0f) - 0.5f) * noiseAmount;
            float y = (Mathf.PerlinNoise(0f, time * noiseSpeed) - 0.5f) * noiseAmount;

            transform.localRotation = Quaternion.Euler(x, y, 0) * Quaternion.identity;
        }
    }

    // ===== FUNCIONES PARA TIMELINE =====

    public void EnableBreathing(bool v) => breathing = v;
    public void EnableBob(bool v) => headBob = v;
    public void EnableNoise(bool v) => microNoise = v;
}
