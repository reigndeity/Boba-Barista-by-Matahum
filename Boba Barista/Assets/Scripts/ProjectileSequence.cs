using UnityEngine;

public class ProjectileSequence : MonoBehaviour
{
    [SerializeField] private int maxLength = 3;
    [SerializeField, Tooltip("Shows current sequence (debug only)")]
    private string debugSequenceView = "";

    public string BubbleSequence { get; private set; } = "";
    public bool CanPressKeys { get; set; } = true;

    public delegate void OnSequenceUpdated(string newSequence);
    public event OnSequenceUpdated SequenceUpdated;

    public void AddToSequence(string value)
    {
        if (!CanPressKeys) return;

        if (BubbleSequence.Length < maxLength)
        {
            BubbleSequence += value;
            debugSequenceView = BubbleSequence;
            //Debug.Log($"Key Added: {value}, Current Sequence: {BubbleSequence}");
            SequenceUpdated?.Invoke(BubbleSequence);
        }
        else
        {
            //Debug.Log("Maximum sequence length reached!");
        }
    }

    public void ClearSequence()
    {
        BubbleSequence = "";
        debugSequenceView = BubbleSequence;
        //Debug.Log("Sequence cleared.");
        SequenceUpdated?.Invoke(BubbleSequence);
    }
}
