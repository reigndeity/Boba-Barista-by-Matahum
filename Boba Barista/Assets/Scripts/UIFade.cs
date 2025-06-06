using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFade : MonoBehaviour
{
    public static UIFade instance { get; private set; }

    [SerializeField] private Image fadeImage;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (fadeImage == null)
            Debug.LogError("Fade Image not assigned.");
    }

    public void Fade(float targetAlpha, float duration)
    {
        StartCoroutine(FadeRoutine(targetAlpha, duration));
    }

    public void FadeOut(float duration)
    {
        // Force starting alpha to 0 when fading out to visible
        SetAlpha(0f);
        Fade(1f, duration);
    }

    public void FadeIn(float duration)
    {
        // Force starting alpha to 1 when fading in to transparent
        SetAlpha(1f);
        Fade(0f, duration);
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        fadeImage.color = new Color(color.r, color.g, color.b, alpha);
    }

    private IEnumerator FadeRoutine(float targetAlpha, float duration)
    {
        float startAlpha = fadeImage.color.a;
        float timer = 0f;

        Color color = fadeImage.color;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, targetAlpha);
    }
}
