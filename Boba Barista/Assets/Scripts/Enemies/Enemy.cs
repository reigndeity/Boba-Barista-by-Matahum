using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMovement m_enemyMovement;
    private EnemySequence m_enemySequence;
    private EnemySpawner m_enemySpawner;
    private EnemyAnimator m_enemyAnimator;
    private UICanvasGroupFade m_uiCanvasGroupFade;
    private Collider m_collider;
    
    

    void Awake()
    {
        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemySequence = GetComponent<EnemySequence>();
        m_enemySpawner = FindFirstObjectByType<EnemySpawner>();
        m_enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        m_uiCanvasGroupFade = GetComponentInChildren<UICanvasGroupFade>();
        m_collider = GetComponent<Collider>();
        
    }
    void Start()
    {
        m_collider.enabled = false;
    }


    public void CorrectSequence()
    {
        StartCoroutine(Correct());
    }
    public void WrongSequence()
    {
        Debug.Log("ANGRY BOY");
    }

    public IEnumerator ChooseLane()
    {   
        m_collider.enabled = true;
        m_enemySequence.GenerateSequence();
        m_enemySequence.SpawnSequenceVisuals();
        m_enemyMovement.ChooseNextLane();
        yield return new WaitForSeconds(0.2f);
        m_uiCanvasGroupFade.FadeIn(1f);
    }

    public IEnumerator Correct()
    {
        m_collider.enabled = false;
        m_enemyAnimator.ChangeAnimationState("enemy_satisfied", 0.25f);
        m_enemyMovement.PauseMovement();
        yield return new WaitForSeconds(2.1f);
        m_enemySpawner.NotifyEnemyDestroyed();
        Destroy(gameObject);
    }


}
