using UnityEngine;

public class AmmoAnimator : MonoBehaviour
{
    public Animator animator;

    private Gun m_gun;
    private ProjectileSequence m_projectileSequence;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_gun = GetComponentInParent<Gun>();
        m_projectileSequence = FindFirstObjectByType<ProjectileSequence>();

        animator.speed = 1.5f;
    }
    public void AmmoShoot()
    {
        animator.SetTrigger("isShoot");
    }
    public void ClearSequence()
    {
        m_projectileSequence.ClearSequence();
    }
    public void ReloadAnimation()
    {
        animator.SetTrigger("isReload");
    }
    public void ResetCanReloadBool()
    {
        m_gun.canReload = true;
    }
}
