using StarterAssets;
using UnityEngine;

public class PlayerController : UnitController
{
    public bool isBlocking;
    public const float maxHp = 100f;
    public float currentHp = maxHp;
    public float attackValue = PlayerConstants.BASE_ATTACK_VALUE;
    public ThirdPersonController thirdPersonController;
    
    public override float GetAttackDamage()
    {
        return attackValue;
    }

    void Update()
    {
        if (currentHp <= 0.0f)
        {
            _animator.SetBool("defeat", true);
            if (!IsDefeatAnimationFinished())
            {
                return;
            }

            stateText.SetActive(true);
            thirdPersonController.enabled = false;
        }
    }


    public override void TakeDamage(UnitController attacker)
    {
        Debug.Log("Player taking damage");
        if (isBlocking)
        {
            attacker.Stagger();
            return;
        }

        currentHp -= attacker.GetAttackDamage();
        healthSlider.value = currentHp / maxHp;
    }

    public override void Stagger()
    {
    }
}