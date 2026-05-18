using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuScene : MonoBehaviour
{
    private Tutorial m_tutorial;
    [Header("Animators")]
    [SerializeField] Animator m_doorAnimator;
    [SerializeField] Animator m_cameraAnimator;
    [Header("Canvas Group")]
    [SerializeField] UICanvasGroupFade m_titleCanvasGroupFade;
    [SerializeField] UICanvasGroupFade m_imageCanvasGroupFade;

    [Header("Main Menu Properties")]
    [SerializeField] Button playButton;

    public GameObject helpPanel;
    [SerializeField] Button helpButton;
    public Button backButton;
    public Button nextButton;

    [SerializeField] GameObject quitConfirmationPanel;
    [SerializeField] Button quitButton;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    void Awake()
    {
        m_tutorial = FindFirstObjectByType<Tutorial>();
    }
    void Start()
    {
        StartCoroutine(MainMenuStart());

        playButton.onClick.AddListener(OnClickPlay);

        helpButton.onClick.AddListener(OnClickHelp);
        backButton.onClick.AddListener(OnClickBack);
        nextButton.onClick.AddListener(OnClickNext);

        quitButton.onClick.AddListener(OnClickQuit);
        yesButton.onClick.AddListener(OnClickQuitYes);
        noButton.onClick.AddListener(OnClickQuitNo);
    }

    public void OnClickPlay()
    {
        StartCoroutine(Play());
        AudioMainMenuScene.instance.PlayButtonClickSound();
    }
    public void OnClickHelp()
    {
        helpPanel.SetActive(true);
        m_tutorial.ResetTutorial();
        AudioMainMenuScene.instance.PlayButtonClickSound();
    }
    public void OnClickBack()
    {
        m_tutorial.Back();
        AudioMainMenuScene.instance.PlayButtonClickSound();
    }
    public void OnClickNext()
    {
        m_tutorial.Next();
        AudioMainMenuScene.instance.PlayButtonClickSound();
    }
    public void OnClickQuit()
    {
        quitConfirmationPanel.SetActive(true);
        AudioMainMenuScene.instance.PlayButtonClickSound();
    }
    public void OnClickQuitYes()
    {
        Application.Quit();
        AudioMainMenuScene.instance.PlayButtonClickSound();
    }
    public void OnClickQuitNo()
    {
        quitConfirmationPanel.SetActive(false);
        AudioMainMenuScene.instance.PlayButtonClickSound();
    }
    public IEnumerator Play()
    {
        Cursor.visible = false;
        playButton.enabled  = false;
        helpButton.interactable = false;
        quitButton.interactable = false;
        m_titleCanvasGroupFade.FadeOut(1.5f);
        m_cameraAnimator.SetTrigger("isPlay");
        yield return new WaitForSeconds(1f);
        m_doorAnimator.SetTrigger("isOpen");
        m_imageCanvasGroupFade.FadeIn(1.5f);
        AudioMainMenuScene.instance.StopGameMusic();
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("GameScene");
    }

    public IEnumerator MainMenuStart()
    {
        AudioMainMenuScene.instance.PlayGameMusic();
        Time.timeScale = 1;
        m_imageCanvasGroupFade.FadeOut(3);
        yield return new WaitForSeconds(1.5f);
        m_titleCanvasGroupFade.FadeIn(2f);
    }
}
