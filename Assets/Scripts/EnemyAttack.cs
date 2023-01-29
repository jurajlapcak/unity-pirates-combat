using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyController enemyController;

    private UnityEngine.AI.NavMeshAgent nav;
    private Animator _animator;
    private bool isClose;
    public AudioSource[] audioSources;
    public GameObject weapon;
    private bool initDone;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (nav.hasPath)
        {
            initDone = true;
        }

        if (enemyController.isStaggered)
        {
            weapon.SetActive(false);
            _animator.SetBool("stagger", true);
            return;
        }

        weapon.SetActive(true);
        _animator.SetBool("stagger", false);

        if (!nav.enabled)
        {
            enemyController.isAttacking = false;
            return;
        }

        isClose = nav.remainingDistance < 0.5f;

        if (!IsIdle() && !IsAnimationFinished("Hit1"))
        {
            return;
        }

        if (!IsIdle() && IsAnimationFinished("Hit1"))
        {
            _animator.SetBool("hit1", false);
            return;
        }

        enemyController.isAttacking = isClose;
        if (isClose && initDone)
        {
            Attack();
            return;
        }

        _animator.SetBool("hit1", false);
    }

    private void Attack()
    {
        int trackSelector = Random.Range(0, audioSources.Length);
        audioSources[trackSelector].Play();
        _animator.SetBool("hit1", true);
    }

    private bool IsAnimationFinished(string animationName)
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
               _animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    private bool IsIdle()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend");
    }
}