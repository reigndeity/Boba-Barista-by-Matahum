using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Collider m_collider;
    private Rigidbody m_rigidbody;
    [Header("Projectile Properties")]
    [SerializeField] float lifetime;

    [SerializeField, Tooltip("Visible in Inspector - the sequence this projectile carries")]
    private string debugSequenceView;
    public string Sequence { get; private set; }

    [Header("Projectile Visuals")]
    private MeshRenderer m_meshRenderer;
    
    [SerializeField] GameObject m_bubbleTeaCup;
    [SerializeField] GameObject m_bubbleParticle;
    [SerializeField] ParticleSystem m_hitParticle;

    [Header("Bubble Tea Layers")]
    [SerializeField] private GameObject[] bubbleTeaLayersObj;
    [Space(10)]
    [SerializeField] private Material[] singleFlavorMaterials;
    [SerializeField] private Material[] doubleFlavorMaterialsL1;
    [SerializeField] private Material[] doubleFlavorMaterialsL2;
    [SerializeField] private Material[] tripleFlavorMaterialsL1;
    [SerializeField] private Material[] tripleFlavorMaterialsL2;
    [SerializeField] private Material[] tripleFlavorMaterialsL3;

    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_collider = GetComponent<Collider>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetSequence(string sequence)
    {
        Sequence = sequence;
        debugSequenceView = sequence;

        UpdateVisuals(sequence);
    }

    private void UpdateVisuals(string currentSequence)
    {

        // --- BUBBLE TEA LAYERS ---
        HideAllBubbleLayers();

        switch (currentSequence.Length)
        {
            case 1:
                bubbleTeaLayersObj[0].SetActive(true);
                ApplyMaterial(bubbleTeaLayersObj[0], singleFlavorMaterials, GetMaterialIndexFromChar(currentSequence[0]));
                break;

            case 2:
                bubbleTeaLayersObj[1].SetActive(true);
                bubbleTeaLayersObj[2].SetActive(true);
                ApplyMaterial(bubbleTeaLayersObj[1], doubleFlavorMaterialsL1, GetMaterialIndexFromChar(currentSequence[0]));
                ApplyMaterial(bubbleTeaLayersObj[2], doubleFlavorMaterialsL2, GetMaterialIndexFromChar(currentSequence[1]));
                break;

            case 3:
                bubbleTeaLayersObj[3].SetActive(true);
                bubbleTeaLayersObj[4].SetActive(true);
                bubbleTeaLayersObj[5].SetActive(true);
                ApplyMaterial(bubbleTeaLayersObj[3], tripleFlavorMaterialsL1, GetMaterialIndexFromChar(currentSequence[0]));
                ApplyMaterial(bubbleTeaLayersObj[4], tripleFlavorMaterialsL2, GetMaterialIndexFromChar(currentSequence[1]));
                ApplyMaterial(bubbleTeaLayersObj[5], tripleFlavorMaterialsL3, GetMaterialIndexFromChar(currentSequence[2]));
                break;
        }
    }

    private int GetMaterialIndexFromChar(char key)
    {
        switch (char.ToUpper(key))
        {
            case 'Q': return 0;
            case 'W': return 1;
            case 'E': return 2;
            case 'R': return 3;
            default: return -1;
        }
    }
    private void ApplyMaterial(GameObject obj, Material[] matList, int index)
    {
        if (index < 0 || index >= matList.Length) return;

        if (obj.TryGetComponent<Renderer>(out var rend))
        {
            rend.material = matList[index];
        }
    }

    private void HideAllBubbleLayers()
    {
        foreach (var obj in bubbleTeaLayersObj)
        {
            obj.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            var enemySeq = collision.gameObject.GetComponentInChildren<EnemySequence>();

            if (enemySeq == null) return;

            if (Sequence == enemySeq.Sequence)
                enemy.CorrectSequence();
            else
                enemy.WrongSequence();

            StartCoroutine(ProjectileHit());
        }
        if (collision.gameObject.CompareTag("Untagged"))
        {
            StartCoroutine(ProjectileHit());
        }
    }

    IEnumerator ProjectileHit()
    {
        m_rigidbody.isKinematic = true;
        m_meshRenderer.enabled = false;
        m_hitParticle.Play();
        m_bubbleParticle.SetActive(false);
        m_bubbleTeaCup.SetActive(false);
        m_collider.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
