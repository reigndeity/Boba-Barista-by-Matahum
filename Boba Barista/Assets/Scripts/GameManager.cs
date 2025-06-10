using System.Collections;
using Febucci.UI;
using Febucci.UI.Effects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private EnemySpawner m_enemySpawner;
    private UIGameScene m_uiGameScene;

    [Header("Game Properties")]
    public int difficulty = 0;
    public bool isGameStart = false;
    public bool isGamePaused;
    public bool canPause = true;
    [SerializeField] TextMeshProUGUI countdownTxt;
    public UICanvasGroupFade m_uiCanvasGroupFade;
    [SerializeField] Animator m_doorAnimator;
    
    [Header("Score Properties")]
    [SerializeField] TextMeshPro playerScoreTxt;
    [SerializeField] TextMeshProUGUI currentScoreTxt;
    [SerializeField] TextMeshProUGUI bestScoreTxt;
    [SerializeField] TextMeshProUGUI ordersCompletedTxt;
    private int ordersCompleted;
    public int playerScore = 0;
    
    [Header("Player Health Properties")]
    public int playerHealth = 5;
    [SerializeField] GameObject healthObj;
    [SerializeField] Material[] healthMaterials;
    [SerializeField] TextMeshPro blackHeartTxt;
    [SerializeField] TextMeshPro redHeartTxt;
    [SerializeField] ShakeBehavior shakeTextProperties;

    void Awake()
    {
        Time.timeScale = 1;
        if (instance != null && instance == this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        m_enemySpawner = FindFirstObjectByType<EnemySpawner>();
        m_uiGameScene = FindFirstObjectByType<UIGameScene>();
        StartCoroutine(GameStart());
        
    }

    public void AddScore()
    {
        ordersCompleted++;
        switch(difficulty)
        {
            case 0:
                playerScore += 100;
                break;
            case 1:
                playerScore += 150;
                break;
            case 2:
                playerScore += 200;
                break;
        }
        playerScoreTxt.text = playerScore.ToString();
        UpdateDifficulty();
    }

    public void UpdateDifficulty()
    {
        if (playerScore < 1000)
        {
            difficulty = 0;
            m_enemySpawner.intervalBetweenPoints = 2.5f;
        }
        else if (playerScore < 2000)
        {
            difficulty = 1;
            m_enemySpawner.intervalBetweenPoints = 1.8f;
        }
        else
        {
            difficulty = 2;
            m_enemySpawner.intervalBetweenPoints = 1f;
        }

        if (playerScore >= 9000)
            m_enemySpawner.maxEnemies = 16;
        else if (playerScore >= 8000)
            m_enemySpawner.maxEnemies = 15;
        else if (playerScore >= 7000)
            m_enemySpawner.maxEnemies = 14;
        else if (playerScore >= 5000)
            m_enemySpawner.maxEnemies = 13;
        else if (playerScore >= 3000)
            m_enemySpawner.maxEnemies = 12;
        else if (playerScore >= 2000)
            m_enemySpawner.maxEnemies = 11;
    }


    public void UpdateHealth()
    {
        playerHealth--;
        MeshRenderer currentHealthMat = healthObj.GetComponent<MeshRenderer>();
        switch (playerHealth)
        {
            case 5:
                currentHealthMat.material = healthMaterials[0];
                redHeartTxt.text = "FFFFF";
                break;
            case 4:
                currentHealthMat.material = healthMaterials[1];
                shakeTextProperties.baseDelay = 0.1f;
                redHeartTxt.text = "<shake>FFFF</shake>";
                break;
            case 3:
                currentHealthMat.material = healthMaterials[2];
                redHeartTxt.text = "<shake>FFF</shake>";
                shakeTextProperties.baseDelay = 0.08f;
                break;
            case 2:
                currentHealthMat.material = healthMaterials[3];
                redHeartTxt.text = "<shake>FF</shake>";
                shakeTextProperties.baseDelay = 0.05f;
                break;
            case 1:
                currentHealthMat.material = healthMaterials[4];
                redHeartTxt.text = "<shake>F</shake>";
                shakeTextProperties.baseAmplitude = 0.05f;
                shakeTextProperties.baseDelay = 0.03f;
                break;
            case 0:
                currentHealthMat.material = healthMaterials[5];
                redHeartTxt.text = "";
                StartCoroutine(GameOver());
                canPause = false;
                break;
        }
    }
    public IEnumerator GameStart()
    {
        Cursor.visible = false;
        m_uiCanvasGroupFade.FadeOut(1.5f);
        canPause = true;
        bestScoreTxt.text = $"{PlayerPrefs.GetInt("HighScore")}";
        yield return new WaitForSeconds(1.75f);
        countdownTxt.text = "3";
        AudioGameScene.instance.PlayReadySound();
        yield return new WaitForSeconds(1);
        countdownTxt.text = "2";
        AudioGameScene.instance.PlayReadySound();
        yield return new WaitForSeconds(1);
        countdownTxt.text = "1";
        AudioGameScene.instance.PlayReadySound();
        yield return new WaitForSeconds(1);
        AudioGameScene.instance.PlayGoSound();
        m_enemySpawner.StartSpawning();
        countdownTxt.text = "SHOOT!";
        isGameStart = true;
        yield return new WaitForSeconds(0.5f);
        AudioGameScene.instance.PlayGameMusic();
        yield return new WaitForSeconds(0.5f);
        m_doorAnimator.SetTrigger("isOpen");
        StartCoroutine(HeartIntro());
        countdownTxt.text = "";
        countdownTxt.enabled = false;
    }
    public IEnumerator HeartIntro()
    {
        blackHeartTxt.text = "<bounce>FFFFF</bounce>";
        redHeartTxt.text = "<bounce>FFFFF</bounce>";
        yield return new WaitForSeconds(1f);
        blackHeartTxt.text = "</bounce>FFFFF</bounce>";
        redHeartTxt.text = "</bounce>FFFFF</bounce>";
    }
    public IEnumerator GameOver()
    {
        Cursor.visible = true;
        ScoreCheck();
        isGameStart = false;
        m_enemySpawner.StopSpawning();
        m_uiGameScene.gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
    }

    public void ScoreCheck()
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestScoreTxt.text = $"{bestScore}";
        if (playerScore > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }
        currentScoreTxt.text = $"{playerScore}";
        ordersCompletedTxt.text = $"{ordersCompleted}";
    }
}
