using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    public float scaleMultiplier = 1.2f;  // How much bigger it grows
    public float duration = 0.3f;         // Total time for the whole pulse
    public bool loop = false;             // Enable for continuous pulsing

    private Vector3 originalScale;
    private bool isPulsing = false;

    void Start()
    {
        originalScale = transform.localScale;

        if (loop)
            StartCoroutine(DoPulse());
    }

    public void TriggerPulse()
    {
        if (!isPulsing)
            StartCoroutine(DoPulse());
    }

    private System.Collections.IEnumerator DoPulse()
    {
        isPulsing = true;

        do
        {
            Vector3 targetScale = originalScale * scaleMultiplier;
            float halfDuration = duration / 2f;
            float timer = 0f;

            // Scale up
            while (timer < halfDuration)
            {
                float t = timer / halfDuration;
                transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
                timer += Time.deltaTime;
                yield return null;
            }

            // Scale down
            timer = 0f;
            while (timer < halfDuration)
            {
                float t = timer / halfDuration;
                transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = originalScale;

            if (!loop)
                isPulsing = false;

        } while (loop);
    }
}
