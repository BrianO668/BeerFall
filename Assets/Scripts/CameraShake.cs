using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    public float goodCatchShakeSpeed;
    public float goodCatchShakeAmount;
    public float goodCatchShakeLength;
    public float goldenBeerShakeSpeed;
    public float goldenBeerShakeAmount;
    public float goldenBeerShakeLength;
    public float badCatchShakeSpeed;
    public float badCatchShakeAmount;
    public float badCatchShakeLength;

    private Vector3 startingPos;
    private Coroutine cameraShakeCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        startingPos = transform.localPosition;
    }

    public void GoodCatchCameraShake()
    {
        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);
            transform.localPosition = startingPos;
        }

        cameraShakeCoroutine = StartCoroutine(CameraShakeIE(goodCatchShakeSpeed, goodCatchShakeAmount, goodCatchShakeLength));
    }

    public void GoldenBeerCameraShake()
    {
        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);
            transform.localPosition = startingPos;
        }

        cameraShakeCoroutine = StartCoroutine(CameraShakeIE(goldenBeerShakeSpeed, goldenBeerShakeAmount, goldenBeerShakeLength));
    }

    public void BadCatchCameraShake()
    {
        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);
            transform.localPosition = startingPos;
        }

        cameraShakeCoroutine = StartCoroutine(CameraShakeIE(badCatchShakeSpeed, badCatchShakeAmount, badCatchShakeLength));
    }

    IEnumerator CameraShakeIE(float speed, float amount, float length)
    {
        float timer = 0f;

        while (timer < length)
        {
            float x = (Mathf.PerlinNoise(Time.time * speed, 0f) - 0.5f) * 2f;
            float y = (Mathf.PerlinNoise(0f, Time.time * speed) - 0.5f) * 2f;

            transform.localPosition = startingPos + new Vector3(x, y, 0f) * amount;

            timer += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = startingPos;
    }
}
