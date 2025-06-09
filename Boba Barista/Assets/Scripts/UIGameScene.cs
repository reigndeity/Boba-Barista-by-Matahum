using System.Collections;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    [Header("Pause Properties")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButtonHolder;
    [SerializeField] GameObject pauseMenuButtonHolder;
    [SerializeField] Button resumeButton;
    [SerializeField] Button pauseRestartButton;
    [SerializeField] Button pauseMenuButton;
    [SerializeField] Button pauseYesButton;
    [SerializeField] Button pauseNoButton;
    [SerializeField] TextMeshProUGUI pauseHeaderText;
    [Header("Game Over Properties")]
    public GameObject gameOverPanel;
    [SerializeField] GameObject valueTextHolder;
    [SerializeField] GameObject gameOverMenuButtonHolder;
    [SerializeField] Button gameOverRestartButton;
    [SerializeField] Button gameOverMenuButton;
    [SerializeField] Button gameOverYesButton;
    [SerializeField] Button gameOverNoButton;
    [SerializeField] TextMeshProUGUI gameOverHeaderText;
    void Start()
    {
        resumeButton.onClick.AddListener(OnClickResume);
        pauseRestartButton.onClick.AddListener(OnClickRestart);
        pauseMenuButton.onClick.AddListener(OnClickPauseMenu);
        pauseYesButton.onClick.AddListener(OnClickPauseMenuYes);
        pauseNoButton.onClick.AddListener(OnClickPauseMenuNo);

        gameOverMenuButton.onClick.AddListener(OnClickGameOverMenu);
        gameOverYesButton.onClick.AddListener(OnClickGameOverMenuYes);
        gameOverNoButton.onClick.AddListener(OnClickGameOverMenuNo);
        gameOverRestartButton.onClick.AddListener(OnClickRestart);
    }
    void Update()
    {
        if (GameManager.instance.canPause == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!GameManager.instance.isGamePaused)
                {
                    GameManager.instance.isGamePaused = true;
                    pausePanel.SetActive(true);
                    Time.timeScale = 0;
                    pauseHeaderText.text = "PAUSED";
                    pauseButtonHolder.SetActive(true);
                    pauseMenuButtonHolder.SetActive(false);
                }
                else
                {
                    GameManager.instance.isGamePaused = false;
                    pausePanel.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }

    public void OnClickResume()
    {
        GameManager.instance.isGamePaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClickRestart()
    {
        StartCoroutine(RestartingTheGame());
    }
    public void OnClickPauseMenu()
    {
        pauseHeaderText.text = "ARE YOU SURE?";
        pauseButtonHolder.SetActive(false);
        pauseMenuButtonHolder.SetActive(true);
    }
    public void OnClickPauseMenuYes()
    {
        StartCoroutine(GoingToMainMenu());
    }
    public void OnClickPauseMenuNo()
    {
        pauseHeaderText.text = "PAUSED";
        pauseButtonHolder.SetActive(true);
        pauseMenuButtonHolder.SetActive(false);
    }

    public void OnClickGameOverMenu()
    {
        gameOverHeaderText.text = "ARE YOU SURE?";
        gameOverMenuButtonHolder.SetActive(true);
        valueTextHolder.SetActive(false);
    }
    public void OnClickGameOverMenuYes()
    {
        StartCoroutine(GoingToMainMenu());
    }
    public void OnClickGameOverMenuNo()
    {
        gameOverHeaderText.text = "GAME OVER";
        valueTextHolder.SetActive(true);
        gameOverMenuButtonHolder.SetActive(false);
    }
    IEnumerator GoingToMainMenu()
    {
        pauseYesButton.enabled = false;
        pauseNoButton.interactable = false;
        GameManager.instance.m_uiCanvasGroupFade.FadeIn(1.5f);
        yield return new WaitForSecondsRealtime(1.51f);
        SceneManager.LoadScene("MainMenuScene");
    }
    IEnumerator RestartingTheGame()
    {
        pauseRestartButton.enabled = false;
        gameOverRestartButton.enabled = false;

        resumeButton.interactable = false;
        pauseMenuButton.interactable = false;
        gameOverMenuButton.interactable = false;
        GameManager.instance.m_uiCanvasGroupFade.FadeIn(1.5f);
        yield return new WaitForSecondsRealtime(1.51f);
        SceneManager.LoadScene("GameScene");
    }
}
