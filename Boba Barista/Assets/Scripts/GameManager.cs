using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private EnemySpawner m_enemySpawner;

    [Header("Game Properties")]
    public int difficulty = 0;
    
    [Header("Score properties")]
    [SerializeField] TextMeshPro playerScoreTxt;
    public int playerScore = 0;

    [Header("Player Health Properties")]
    private int playerHealth = 5;
    [SerializeField] GameObject healthObj;
    [SerializeField] Material[] healthMaterials;
    [SerializeField] TextMeshPro blackHeartTxt;
    [SerializeField] TextMeshPro redHeartTxt;

    void Awake()
    {
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
    }

    public void AddScore()
    {
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
                redHeartTxt.text = "FFFF";
                break;
            case 3:
                currentHealthMat.material = healthMaterials[2];
                redHeartTxt.text = "FFF";
                break;
            case 2:
                currentHealthMat.material = healthMaterials[3];
                redHeartTxt.text = "FF";
                break;
            case 1:
                currentHealthMat.material = healthMaterials[4];
                redHeartTxt.text = "F";
                break;
            case 0:
                currentHealthMat.material = healthMaterials[5];
                redHeartTxt.text = "";
                break;
        }
        
    }
}
