using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float shakeSpeed;
    public float shakeAmount;
    public float pulseSpeed;
    public float pulseAmount;

    private Vector3 startingPos;
    private Vector3 startingScale;
    void Awake()
    {
        startingPos = transform.localPosition;
        startingScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Perlin noise shake. Perlin noise returns 0-1. This math gives us -1 to 1
        float x = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f) * 2f;
        float y = (Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f) * 2f;

        transform.localPosition = startingPos + new Vector3(x, y, 0f) * shakeAmount;

        //Breathing effect
        float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        transform.localScale = startingScale * scale;
    }

    private void OnDisable()
    {
        transform.localPosition = startingPos;
        transform.localScale = startingScale;
    }
}
