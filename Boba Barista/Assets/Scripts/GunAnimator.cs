using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    public Animator animator;
    private GunShooting m_gunShooting;

    void Awake()
    {
        animator = GetComponent<Animator>();
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
}
