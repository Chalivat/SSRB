using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;
    float initialDuration;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    private void Start()
    {
        initialDuration = shakeDuration;
    }
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        TimeGo();
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = initialDuration;
            camTransform.localPosition = originalPos;
        }
    }

    public static void ShakeCamera(float duration, float amount)
    {
        Camera.main.gameObject.GetComponent<CameraShake>().shakeDuration = duration;
        Camera.main.gameObject.GetComponent<CameraShake>().shakeAmount = amount;
    }

    public static void FreezeTime(int frame)
    {
        Camera.main.gameObject.GetComponent<CameraShake>().TimeStop(frame);
    }
    private int frameCount = 5;
     
        void TimeStop(int frame)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            frameCount = frame;
        }

        void TimeGo()
        {
            if (frameCount < 5)
            {
                frameCount++;
            }
            else
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
            }
                    
        }
    
}

