using UnityEngine;
using System.Collections;

public class AudioMainMenuScene : MonoBehaviour
{
    public static AudioMainMenuScene instance;

    [Header("Music Properties")]
    public AudioSource musicSource;
    public AudioClip[] musicClip;

    [Header("Sfx Properties")]
    public AudioSource sfxSource;
    public AudioClip[] sfxClip;

    private Coroutine fadeCoroutine;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void PlayGameMusic()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        musicSource.clip = musicClip[0];
        musicSource.volume = 0f;
        musicSource.loop = true;
        musicSource.Play();
        fadeCoroutine = StartCoroutine(FadeInMusic(1.5f, 0.75f));
    }

    public void StopGameMusic()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutMusic(1.5f));
    }

    private IEnumerator FadeInMusic(float duration, float targetVolume)
    {
        float timer = 0f;
        while (timer < duration)
        {
            musicSource.volume = Mathf.Lerp(0f, targetVolume, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        musicSource.volume = targetVolume;
    }

    private IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;
        float timer = 0f;
        while (timer < duration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        musicSource.volume = 0f;
        musicSource.Stop();
    }

    public void PlayButtonClickSound()
    {
        int randomNumber = Random.Range(0, 3);
        sfxSource.PlayOneShot(sfxClip[randomNumber]);
    }
}
