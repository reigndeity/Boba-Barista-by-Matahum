using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Properties")]
    public int difficulty = 0;

    void Awake()
    {
        if (instance != null && instance == this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
