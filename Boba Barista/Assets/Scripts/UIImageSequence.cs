using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIImageSequence : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI redHeartTxt;
    [Header("Animation Settings")]
    public Sprite[] frames;
    public float framesPerSecond = 12f;
    public bool loop = true;
    public bool playOnStart = true;

    [Header("Target Image")]
    public Image targetImage;

    private int currentFrame;
    private float timer;
    private bool isPlaying;

    void Start()
    {
        if (playOnStart) Play();
    }

    void Update()
    {
        if (!isPlaying || frames.Length == 0 || targetImage == null)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f / framesPerSecond)
        {
            timer -= 1f / framesPerSecond;
            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                if (loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    isPlaying = false;
                    currentFrame = frames.Length - 1;
                }
            }

            // Set the sprite
            targetImage.sprite = frames[currentFrame];

            // Frame-specific behavior
            switch (currentFrame)
            {
                case 0:
                    redHeartTxt.text = "FFFFF";
                    break;
                case 1:
                    redHeartTxt.text = "FFFF";
                    break;
                case 2:
                    redHeartTxt.text = "FFF";
                    break;
                case 3:
                    redHeartTxt.text = "FF";
                    break;
                case 4:
                    redHeartTxt.text = "F";
                    break;
                case 5:
                    redHeartTxt.text = "";
                    break;
            }
        }
    }

    public void Play()
    {
        if (frames.Length > 0)
        {
            isPlaying = true;
            currentFrame = 0;
            timer = 0f;
            targetImage.sprite = frames[0];

            // Optional: Add logic for first frame
            Debug.Log("Animation started on frame 0");
        }
    }

    public void Stop()
    {
        isPlaying = false;
    }

    public void SetFrames(Sprite[] newFrames)
    {
        frames = newFrames;
        currentFrame = 0;
        timer = 0f;
    }
}
