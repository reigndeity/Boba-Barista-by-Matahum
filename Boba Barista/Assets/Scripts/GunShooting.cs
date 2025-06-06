using UnityEngine;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce = 30f;

    private ProjectileSequence m_projectileSequence;

    void Start()
    {
        m_projectileSequence = GetComponentInChildren<ProjectileSequence>();
    }

    public void Shoot()
    {
        if (m_projectileSequence.BubbleSequence.Length == 0)
            return;

        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        if (proj.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }

        if (proj.TryGetComponent(out Projectile projectileScript))
        {
            projectileScript.SetSequence(m_projectileSequence.BubbleSequence);
        }

        m_projectileSequence.ClearSequence();
    }
}
