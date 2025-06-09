using UnityEngine;
using System.Collections;

public class AudioGameScene : MonoBehaviour
{
    public static AudioGameScene instance;

    [Header("Music Properties")]
    public AudioSource musicSource;
    public AudioClip[] musicClip;

    [Header("Sfx Properties")]
    public AudioSource sfxGameSource;
    public AudioClip[] sfxGameClip;
    public AudioSource sfxUISource;
    public AudioClip[] sfxUIClip;
    public AudioSource sfxPoofSource;
    public AudioSource sfxShootSource;

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

        musicSource.volume = 0.75f;
        musicSource.loop = true;
        int randomNumber = Random.Range(0, 2);
        musicSource.clip = musicClip[randomNumber];
        musicSource.Play();
    }

    public void StopGameMusic()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutMusic(1.5f));
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
        sfxUISource.PlayOneShot(sfxUIClip[randomNumber]);
    }

    public void PlayReadySound() => sfxUISource.PlayOneShot(sfxUIClip[3]);
    public void PlayGoSound() => sfxUISource.PlayOneShot(sfxUIClip[4]);

    public void PlayGunShootSound() => sfxShootSource.PlayOneShot(sfxGameClip[0]);
    public void PlayProjectileHitSound() => sfxGameSource.PlayOneShot(sfxGameClip[1]);
    public void PlayReloadSound() => sfxGameSource.PlayOneShot(sfxGameClip[2]);
    public void PlayPoofSound() => sfxPoofSource.PlayOneShot(sfxGameClip[3]);
    public void PlayAngrySound() => sfxGameSource.PlayOneShot(sfxGameClip[4]);
    public void PlayFlavorClickSound() => sfxGameSource.PlayOneShot(sfxGameClip[5]);
}
