using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public EnemyController enemyController;

    private Vector3 playerBounds;
    private UnityEngine.AI.NavMeshAgent nav;
    private Animator _animator;


    void Awake()
    {
        playerBounds = player.GetComponent<Collider>().bounds.size;
        playerController = player.GetComponent<PlayerController>();
        enemyController = GetComponent<EnemyController>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemyController.isAttacking)
        {
            _animator.SetFloat("Speed", 0.0f);
            _animator.SetFloat("MotionSpeed", 1.0f);
        }

        if (enemyController.currentHp <= 0 || playerController.currentHp <= 0 || enemyController.isStaggered)
        {
            nav.enabled = false;
            return;
        }

        nav.enabled = true;

        Vector3 goal = player.position;
        goal.z -= (playerBounds.z + 1f);
        nav.SetDestination(goal);
        transform.LookAt(player);

        _animator.SetFloat("Speed", nav.remainingDistance > 0.0 ? 1.0f : 0.0f);
        _animator.SetFloat("MotionSpeed", nav.remainingDistance > 0.0 ? nav.speed : 1.0f);
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
    }

    private void OnLand(AnimationEvent animationEvent)
    {
    }
}