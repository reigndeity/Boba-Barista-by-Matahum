using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    public Animator animator;
    private GunShooting m_gunShooting;
    private GunVisuals m_gunVisuals;

    void Awake()
    {
        animator = GetComponent<Animator>();
        m_gunVisuals = GetComponentInParent<GunVisuals>();
    }
    void Start()
    {
        m_gunShooting = GetComponentInParent<GunShooting>();
        animator.speed = 1.5f;
    }
    public void ShootAnimation()
    {
        animator.SetTrigger("isShooting");
    }
    public void Shoot()
    {
        m_gunShooting.Shoot();
    }

    public void GunDistorionParticle()
    {
        m_gunVisuals.PlayGunDistorion();
    }
}
