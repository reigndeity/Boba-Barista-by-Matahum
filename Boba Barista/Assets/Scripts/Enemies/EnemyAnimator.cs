using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator m_animator;
    private string currentAnimationState;
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    public void ChangeAnimationState(string newAnimationState, float transitionDuration = 0.25f)
    {
        if (currentAnimationState == newAnimationState) return;
        m_animator.CrossFade(newAnimationState, transitionDuration);
        currentAnimationState = newAnimationState;
    }
}
