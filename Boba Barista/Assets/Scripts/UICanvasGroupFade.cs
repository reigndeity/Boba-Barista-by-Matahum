using UnityEngine;

public class UICanvasGroupFade : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;

    private Coroutine currentFade;

    void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Fades the CanvasGroup to invisible (alpha 0).
    /// From color to clear.
    /// </summary>
    public void FadeOut(float duration)
    {
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeCanvasGroup(1f, 0f, duration)); // color to clear
    }

    /// <summary>
    /// Fades the CanvasGroup to visible (alpha 1).
    /// From clear to color.
    /// </summary>
    public void FadeIn(float duration)
    {
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeCanvasGroup(0f, 1f, duration)); // clear to color
    }

    private System.Collections.IEnumerator FadeCanvasGroup(float from, float to, float duration)
    {
        float time = 0f;
        m_canvasGroup.alpha = from;
        m_canvasGroup.blocksRaycasts = to > 0.5f;
        m_canvasGroup.interactable = to > 0.5f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // ✨ This makes it independent of Time.timeScale
            m_canvasGroup.alpha = Mathf.Lerp(from, to, time / duration);
            yield return null;
        }

        m_canvasGroup.alpha = to;
    }
}
