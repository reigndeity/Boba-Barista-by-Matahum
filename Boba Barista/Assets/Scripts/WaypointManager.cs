using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    [Header("Common Path")]
    public Transform centerWaypoint;

    [Header("Lane Paths")]
    public Transform[] laneA;
    public Transform[] laneB;
    public Transform[] laneC;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Transform GetCenterPoint()
    {
        return centerWaypoint;
    }

    public Transform[][] GetAllLanes()
    {
        return new Transform[][] { laneA, laneB, laneC };
    }
}
