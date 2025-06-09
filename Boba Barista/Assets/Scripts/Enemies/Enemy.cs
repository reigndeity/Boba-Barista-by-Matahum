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
    
    private bool m_isAngry = false;
    [SerializeField] ParticleSystem m_poofParticle;
    [SerializeField] ParticleSystem m_angryParticle;
    [SerializeField] GameObject m_enemyModel;

    [SerializeField] LayerMask projectileLayer;
    private Coroutine m_correctCoroutine;
    private Coroutine m_wrongCoroutine;

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
        if (m_wrongCoroutine != null)
        {
            StopCoroutine(m_wrongCoroutine);
            m_wrongCoroutine = null;
        }
        if (m_correctCoroutine != null)
        {
            StopCoroutine(m_correctCoroutine);
            m_correctCoroutine = null;
        }

        m_correctCoroutine = StartCoroutine(Correct());
    }
    public void WrongSequence()
    {
        if (!m_isAngry)
        {
            m_isAngry = true;
            if (m_correctCoroutine != null)
            {
                StopCoroutine(m_correctCoroutine);
                m_correctCoroutine = null;
            }
            if (m_wrongCoroutine != null) 
            {
                StopCoroutine(m_wrongCoroutine);
                m_wrongCoroutine = null;
            }

            m_wrongCoroutine = StartCoroutine(Wrong());
        }
        else
        {
            Debug.Log("IM ALREADY ANGRY");
        }
        
    }

    public IEnumerator ChooseLane()
    {   
        m_enemySequence.GenerateSequence();
        m_enemySequence.SpawnSequenceVisuals();
        m_enemyMovement.ChooseNextLane();
        yield return new WaitForSeconds(0.2f);
        m_collider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        m_uiCanvasGroupFade.FadeIn(0.1f);
    }

    public IEnumerator Correct()
    {
        GameManager.instance.AddScore();
        m_collider.excludeLayers = projectileLayer;
        m_enemyAnimator.ChangeAnimationState("enemy_satisfied", 0.1f);
        m_enemyMovement.PauseMovement();
        m_uiCanvasGroupFade.FadeOut(0.1f);
        yield return new WaitForSeconds(2.01f);
        m_enemyModel.SetActive(false);
        m_poofParticle.Play();
        m_enemySpawner.NotifyEnemyDestroyed();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public IEnumerator Wrong()
    {
        m_enemyMovement.PauseMovement();
        m_enemyAnimator.ChangeAnimationState("enemy_angry", 0.25f);
        m_angryParticle.Play();
        yield return new WaitForSeconds(2.01f);
        m_enemyMovement.speed = 4.5f;
        m_enemyAnimator.ChangeAnimationState("enemy_angryWalk", 0.25f);
        m_enemyMovement.ContinueMovement();
    }
    public IEnumerator LoseHealth()
    {
        GameManager.instance.UpdateHealth();
        m_collider.excludeLayers = projectileLayer;
        m_enemyMovement.PauseMovement();
        m_enemyAnimator.ChangeAnimationState("enemy_angry", 0.25f);
        m_uiCanvasGroupFade.FadeOut(0.1f);
        yield return new WaitForSeconds(2.01f);
        m_enemyModel.SetActive(false);
        m_poofParticle.Play();
        m_enemySpawner.NotifyEnemyDestroyed();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
