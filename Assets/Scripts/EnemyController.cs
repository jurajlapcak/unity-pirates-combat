using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemyController : UnitController
{
    public bool isStaggered;
    public float staggerTime = 4f;
    public float staggeredAt;
    public GameManager gameManager;
    public float maxHp = 200f;
    public float currentHp = 200f;
    public float attackValue = 10f;
    public AudioSource dyingAudio;
    private bool playedDyingAudio;
    
    public override float GetAttackDamage()
    {
        return attackValue;
    }

    void Start()
    {
        if (healthSlider != null)
        {
            healthSlider.value = 1;
        }

        currentHp = maxHp;
        gameManager.MustNotBeNull();
    }

    void Update()
    {
        if (currentHp <= 0.0f)
        {
            _animator.SetBool("deafeat", true);
            if (dyingAudio != null && !playedDyingAudio)
            {
                dyingAudio.Play();
                playedDyingAudio = true;
            }

            if (!IsDefeatAnimationFinished())
            {
                return;
            }

            if (!gameManager.switchLock)
            {
                gameManager.deadObjects++;
                gameObject.SetActive(false);
            }

            if (stateText != null)
            {
                stateText.SetActive(true);
            }
        }

        if (Time.time - staggeredAt > staggerTime)
        {
            isStaggered = false;
        }
    }

    public override void Stagger()
    {
        isStaggered = true;
        isAttacking = false;
        staggeredAt = Time.time;
    }


    public override void TakeDamage(UnitController attacker)
    {
        currentHp -= attacker.GetAttackDamage();
        Debug.Log("Enemy taking damage: " + attacker.GetAttackDamage() + " new hp: " + currentHp);
        Debug.Log(healthSlider);
        if (healthSlider != null)
        {
            healthSlider.value = currentHp / maxHp;
        }
    }
}