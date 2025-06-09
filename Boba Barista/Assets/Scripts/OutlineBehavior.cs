using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Required for Button

public class OutlineBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Font Asset to use when hovering")]
    public TMP_FontAsset hoverFont;

    private TextMeshProUGUI tmpText;
    private TMP_FontAsset originalFont;
    private Button button;

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalFont = tmpText.font;
        button = GetComponentInParent<Button>(); // Assumes TMP text is inside the button
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable && button.enabled && hoverFont != null)
        {
            tmpText.font = hoverFont;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpText.font = originalFont;
    }
}
