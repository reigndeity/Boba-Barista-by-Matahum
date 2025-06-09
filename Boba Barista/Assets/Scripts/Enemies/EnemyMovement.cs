using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private UICanvasGroupFade m_uiCanvasGroupFade;
    private Enemy m_enemy;

    [Header("Phase 1: Go to Center")]
    private Transform[] centerPath;

    [Header("Phase 2: Lane Waypoints")]
    private Transform[][] lanePaths;

    [Header("Movement Settings")]
    public float speed = 2f;
    public float reachDistance = 0.1f;
    public bool loop = false;

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f;

    [Header("Raycast Detection")]
    public float enemyDetectDistance = 1f;
    public LayerMask enemyLayer;

    [Space(10)]
    public float playerBorderDetectionDistance = 2f;
    public LayerMask playerBorderLayer;

    private int currentWaypointIndex = 0;
    private Transform[] currentPath;
    private bool isPaused = false;
    private bool hasChosenLane = false;

    private void Start()
    {
        m_enemy = GetComponentInParent<Enemy>();
        centerPath = new Transform[] { WaypointManager.Instance.GetCenterPoint() };
        currentPath = centerPath;
        lanePaths = WaypointManager.Instance.GetAllLanes();
    }

    private void Update()
    {
        if (isPaused || currentPath.Length == 0) return;

        Transform target = currentPath[currentWaypointIndex];
        Vector3 direction = (target.position - transform.position).normalized;

        // Stop or react if near player border
        if (IsNearPlayerBorder(direction))
        {
            StartCoroutine(m_enemy.LoseHealth());
        }

        // Only move if path is clear
        if (!IsBlocked(direction))
        {
            // Move toward target
            transform.position += direction * speed * Time.deltaTime;

            // Rotate (adjust if model faces -Z)
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation * Quaternion.Euler(0, 180f, 0),
                    rotationSpeed * Time.deltaTime
                );
            }

            // Check if target reached
            if (Vector3.Distance(transform.position, target.position) <= reachDistance)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex >= currentPath.Length)
                {
                    if (!hasChosenLane)
                    {
                        m_enemy.StartCoroutine(m_enemy.ChooseLane());
                    }
                    else
                    {
                        if (loop)
                            currentWaypointIndex = 0;
                        else
                            enabled = false;
                    }
                }
            }
        }

        if (GameManager.instance.playerHealth == 0)
        {
            PauseMovement();
        }
    }

    private bool IsBlocked(Vector3 direction)
    {
        // Raycast to detect obstacles
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, direction);
        return Physics.Raycast(ray, enemyDetectDistance, enemyLayer);
    }

    private bool IsNearPlayerBorder(Vector3 direction)
    {
        // Raycast to detect player border
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, direction);
        return Physics.Raycast(ray, playerBorderDetectionDistance, playerBorderLayer);
    }

    public void ChooseNextLane()
    {
        int chosenLane = Random.Range(0, lanePaths.Length);
        currentPath = lanePaths[chosenLane];
        currentWaypointIndex = 0;
        hasChosenLane = true;
    }

    public void PauseMovement() => isPaused = true;
    public void ContinueMovement() => isPaused = false;

    private void OnDrawGizmosSelected()
    {
        if (currentPath != null && currentWaypointIndex < currentPath.Length)
        {
            Vector3 dir = (currentPath[currentWaypointIndex].position - transform.position).normalized;

            // Obstacle detection ray (red)
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * 0.5f,
                            transform.position + Vector3.up * 0.5f + dir * enemyDetectDistance);

            // Player border detection ray (blue)
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + Vector3.up * 0.5f,
                            transform.position + Vector3.up * 0.5f + dir * playerBorderDetectionDistance);
        }
    }
}
