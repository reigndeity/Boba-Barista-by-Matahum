using UnityEngine;

public class EnemySequence : MonoBehaviour
{
    [SerializeField, Tooltip("Current difficulty: 0 = 1-letter, 1 = 2-letter, 2 = 3-letter")]
    private int difficulty;

    [SerializeField, Tooltip("Generated sequence (e.g., qwe)")]
    private string sequence = "";

    public string Sequence => sequence; // public getter

    private readonly char[] keys = { 'Q', 'W', 'E', 'R' };

    [Header("Visuals")]
    [SerializeField, Tooltip("Prefabs for q, w, e, r in this order")]
    private GameObject[] prefabs; // assign 4 prefabs in inspector

    [SerializeField, Tooltip("Spawn point where prefabs will be instantiated")]
    private Transform spawnPoint;

    void Start()
    {
        difficulty = GameManager.instance.difficulty;
    }

    public void GenerateSequence()
    {
        int sequenceLength = Mathf.Clamp(difficulty + 1, 1, 3);
        sequence = "";

        for (int i = 0; i < sequenceLength; i++)
        {
            char randomKey = keys[Random.Range(0, keys.Length)];
            sequence += randomKey;
        }

        Debug.Log($"[EnemySequence] Generated: {sequence.ToUpper()}");
    }

    public void SetDifficulty(int newDifficulty)
    {
        difficulty = Mathf.Clamp(newDifficulty, 0, 2);
        GenerateSequence();
        SpawnSequenceVisuals();
    }

    public void SpawnSequenceVisuals()
    {
        // Clear previous children to avoid clutter (optional)
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }

        float offsetX = 0f;
        float spacing = 1.5f; // spacing between spawned prefabs

        foreach (char c in sequence)
        {
            GameObject prefab = GetPrefabFromChar(c);
            if (prefab != null)
            {
                Vector3 spawnPos = spawnPoint.position + new Vector3(offsetX, 0, 0);
                Instantiate(prefab, spawnPos, Quaternion.identity, spawnPoint);
                offsetX += spacing;
            }
            else
            {
                Debug.LogWarning($"No prefab mapped for character: {c}");
            }
        }
    }

    private GameObject GetPrefabFromChar(char c)
    {
        switch (c)
        {
            case 'Q': return prefabs.Length > 0 ? prefabs[0] : null;
            case 'W': return prefabs.Length > 1 ? prefabs[1] : null;
            case 'E': return prefabs.Length > 2 ? prefabs[2] : null;
            case 'R': return prefabs.Length > 3 ? prefabs[3] : null;
            default: return null;
        }
    }
}
