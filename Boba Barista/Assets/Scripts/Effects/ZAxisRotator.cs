using UnityEngine;

public class ZAxisRotator : MonoBehaviour
{
    public float minZRotation = -15f;   // Minimum Z angle
    public float maxZRotation = 15f;    // Maximum Z angle
    public float rotationSpeed = 1f;    // Speed of rotation
    public bool loop = false;           // Enable for continuous oscillation

    private Quaternion startRotation;
    private Coroutine rotationCoroutine;

    void Awake()
    {
        startRotation = transform.localRotation;
    }

    void OnEnable()
    {
        if (loop)
        {
            // Restart coroutine if looping is enabled
            rotationCoroutine = StartCoroutine(RotateZLoop());
        }
    }

    void OnDisable()
    {
        // Clean up coroutine if the object gets disabled
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }

        transform.localRotation = startRotation; // Reset to original rotation if needed
    }

    public void TriggerRotation()
    {
        if (!loop && rotationCoroutine == null)
        {
            rotationCoroutine = StartCoroutine(RotateZOnce());
        }
    }

    private System.Collections.IEnumerator RotateZOnce()
    {
        float timer = 0f;
        float duration = 1f / rotationSpeed;
        Quaternion from = Quaternion.Euler(0, 0, minZRotation);
        Quaternion to = Quaternion.Euler(0, 0, maxZRotation);

        // Rotate from min to max
        while (timer < duration)
        {
            float t = timer / duration;
            transform.localRotation = Quaternion.Lerp(from, to, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Rotate back to original
        timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            transform.localRotation = Quaternion.Lerp(to, startRotation, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = startRotation;
        rotationCoroutine = null;
    }

    private System.Collections.IEnumerator RotateZLoop()
    {
        while (loop)
        {
            yield return RotateBetweenAngles(minZRotation, maxZRotation);
            yield return RotateBetweenAngles(maxZRotation, minZRotation);
        }
    }

    private System.Collections.IEnumerator RotateBetweenAngles(float fromZ, float toZ)
    {
        float timer = 0f;
        float duration = 1f / rotationSpeed;
        Quaternion from = Quaternion.Euler(0, 0, fromZ);
        Quaternion to = Quaternion.Euler(0, 0, toZ);

        while (timer < duration)
        {
            float t = timer / duration;
            transform.localRotation = Quaternion.Lerp(from, to, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = to;
    }
}
