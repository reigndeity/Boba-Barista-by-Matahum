using UnityEngine;

public class GunVisuals : MonoBehaviour
{
    [Header("Gun Circle Visuals")]
    [SerializeField] private GameObject[] indicators; // 3 circles
    [SerializeField] private Material[] indicatorMaterials; // Q, W, E, R, White fallback
    private ProjectileSequence sequence;

    [Header("Bubble Tea Layer Visuals")]
    [SerializeField] private GameObject[] bubbleTeaLayersObj;
    [Space(10)]
    [SerializeField] private Material[] singleFlavorMaterials;
    [SerializeField] private Material[] doubleFlavorMaterialsL1;
    [SerializeField] private Material[] doubleFlavorMaterialsL2;
    [SerializeField] private Material[] tripleFlavorMaterialsL1;
    [SerializeField] private Material[] tripleFlavorMaterialsL2;
    [SerializeField] private Material[] tripleFlavorMaterialsL3;

    private void Awake()
    {
        sequence = GetComponent<ProjectileSequence>();
        if (sequence == null)
        {
            Debug.LogError("ProjectileSequence not found on GameObject.");
        }
    }

    private void OnEnable()
    {
        if (sequence != null)
        {
            sequence.SequenceUpdated += UpdateVisuals;
        }
    }

    private void OnDisable()
    {
        if (sequence != null)
        {
            sequence.SequenceUpdated -= UpdateVisuals;
        }
    }

    private void UpdateVisuals(string currentSequence)
    {
        // --- CIRCLE INDICATORS ---
        for (int i = 0; i < indicators.Length; i++)
        {
            SetIndicatorMaterial(indicators[i], indicatorMaterials[4]); // White fallback
        }

        for (int i = 0; i < currentSequence.Length && i < indicators.Length; i++)
        {
            char key = currentSequence[i];
            int matIndex = GetMaterialIndexFromChar(key);
            if (matIndex != -1)
            {
                SetIndicatorMaterial(indicators[i], indicatorMaterials[matIndex]);
            }
        }

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

    private void SetIndicatorMaterial(GameObject obj, Material mat)
    {
        if (obj.TryGetComponent<Renderer>(out var rend))
        {
            rend.material = mat;
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
}
