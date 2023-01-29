using UnityEngine;
using UnityEngine.UI;

public abstract class UnitController : MonoBehaviour
{
    public bool isAttacking = false;
    public Animator _animator;
    public Slider healthSlider;
    public GameObject stateText;

    public abstract float GetAttackDamage();
    public abstract void TakeDamage(UnitController attacker);
    public abstract void Stagger();

    protected void Awake()
    {
        _animator = GetComponent<Animator>();

        if (stateText != null)
        {
            stateText.SetActive(false);
        }
    }

    protected bool IsDefeatAnimationFinished()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.89f &&
               _animator.GetCurrentAnimatorStateInfo(0).IsName("Defeat");
    }
}