using UnityEngine;

public class BounceWave : MonoBehaviour
{
    public GameObject[] objectsToBounce;
    public float bounceHeight = 0.5f;  // max bounce height
    public float bounceSpeed = 2f;     // speed of bounce cycle
    public float bounceDelay = 0.2f;   // delay between each object's bounce start

    private Vector3[] initialPositions;

    void Start()
    {
        initialPositions = new Vector3[objectsToBounce.Length];
        for (int i = 0; i < objectsToBounce.Length; i++)
        {
            initialPositions[i] = objectsToBounce[i].transform.position;
        }
    }

    void Update()
    {
        for (int i = 0; i < objectsToBounce.Length; i++)
        {
            float timeOffset = Time.time * bounceSpeed - i * bounceDelay;
            float yOffset = Mathf.Abs(Mathf.Sin(timeOffset)) * bounceHeight;
            Vector3 newPos = initialPositions[i] + new Vector3(0, yOffset, 0);
            objectsToBounce[i].transform.position = newPos;
        }
    }
}
