using UnityEngine;

public class IngredientButtons : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] Animator q_Animator;
    [SerializeField] Animator w_Animator;
    [SerializeField] Animator e_Animator;
    [SerializeField] Animator r_Animator;
    public void QButtonPressed() => q_Animator.SetTrigger("isPressed");
    public void WButtonPressed() => w_Animator.SetTrigger("isPressed");
    public void EButtonPressed() => e_Animator.SetTrigger("isPressed");
    public void RButtonPressed() => r_Animator.SetTrigger("isPressed");
}
