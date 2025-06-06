using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] float lifetime;

    [SerializeField, Tooltip("Visible in Inspector - the sequence this projectile carries")]
    private string debugSequenceView;

    public string Sequence { get; private set; }

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                EnemySequence enemySeq = other.GetComponentInChildren<EnemySequence>();

                string enemySequence = enemySeq.Sequence;

                //Debug.Log($"Projectile [{Sequence}] vs Enemy [{enemySequence}]");

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

                Destroy(gameObject); // Always destroy projectile after hit
            }
    }
}
