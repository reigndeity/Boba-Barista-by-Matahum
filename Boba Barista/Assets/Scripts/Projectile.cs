using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] float lifetime;

    [SerializeField, Tooltip("Visible in Inspector - the sequence this projectile carries")]
    private string debugSequenceView;

    public string Sequence { get; private set; }

    [Header("Projectile Visuals")]
    private MeshRenderer m_meshRenderer;
    private ParticleSystem m_particleSystem;

    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    void Start()
    {
        Destroy(gameObject, lifetime);
        //Debug.Log($"Projectile spawned with sequence: {Sequence}");
    }

    public void SetSequence(string sequence)
    {
        Sequence = sequence;
        debugSequenceView = sequence; // show in Inspector
        //Debug.Log($"Projectile sequence set to: {Sequence}");

        // You can use the sequence here to apply effects, change visuals, etc.
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            EnemySequence enemySeq = collision.gameObject.GetComponentInChildren<EnemySequence>();

            if (enemySeq == null)
            {
                Debug.LogWarning("❗ EnemySequence not found on enemy!");
                return;
            }

            string enemySequence = enemySeq.Sequence;

            if (Sequence == enemySequence)
            {
                Debug.Log("✅ Match!");
                enemy.CorrectSequence(); // delegate handling
            }
            else
            {
                Debug.Log("❌ Mismatch!");
                enemy.WrongSequence(); // delegate handling
            }

            StartCoroutine(ProjectileHit());
        }
    }

    IEnumerator ProjectileHit()
    {
        m_meshRenderer.enabled = false;
        m_particleSystem.Play();
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject); // Always destroy projectile after hit
    }
    
}
