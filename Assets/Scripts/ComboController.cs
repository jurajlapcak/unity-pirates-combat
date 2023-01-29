using UnityEngine;

public class ComboController : MonoBehaviour
{
    public PlayerController playerController;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource block;

    private Animator _animator;
    private float lastClickedTime = 0f;
    private float maxComboDelay = 1f;

    private bool playNextAnimation;
    private bool isBlocking;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            block.Play();
            isBlocking = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    void FixedUpdate()
    {
        if (isBlocking)
        {
            playNextAnimation = false;
        }

        IsPlayingHit();
        Combo();
        Block();

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            playNextAnimation = false;
        }
    }


    void Block()
    {
        playerController.isBlocking = isBlocking;
        if (!isBlocking)
        {
            _animator.SetBool("block", false);
            return;
        }

        _animator.SetBool("block", true);
    }

    void Combo()
    {
        if (!playNextAnimation)
        {
            _animator.SetBool("hit1", false);
            _animator.SetBool("hit2", false);
            _animator.SetBool("hit3", false);
            return;
        }

        if (IsIdle())
        {
            _animator.SetBool("hit1", true);
            _animator.SetBool("hit2", false);
            _animator.SetBool("hit3", false);
            audioSource1.Play();
        }
        else if (IsComboAnimationFinished("Hit1"))
        {
            _animator.SetBool("hit1", false);
            _animator.SetBool("hit2", true);
            _animator.SetBool("hit3", false);
            audioSource2.Play();
        }
        else if (IsComboAnimationFinished("Hit2"))
        {
            _animator.SetBool("hit1", false);
            _animator.SetBool("hit2", false);
            _animator.SetBool("hit3", true);
            audioSource3.Play();
        }

        else if (IsComboAnimationFinished("Hit2"))
        {
            _animator.SetBool("hit1", true);
            _animator.SetBool("hit2", false);
            _animator.SetBool("hit3", false);
            audioSource1.Play();
        }
        else
        {
            _animator.SetBool("hit1", false);
            _animator.SetBool("hit2", false);
            _animator.SetBool("hit3", false);
        }
    }

    private void IsPlayingHit()
    {
        if (IsPlayingAnimation("Hit1"))
        {
            playerController.isAttacking = true;
            playerController.attackValue = PlayerConstants.BASE_ATTACK_VALUE * PlayerConstants.HIT1_MULTIPLIER;
        }
        else if (IsPlayingAnimation("Hit2"))
        {
            playerController.isAttacking = true;
            playerController.attackValue = PlayerConstants.BASE_ATTACK_VALUE * PlayerConstants.HIT2_MULTIPLIER;
        }
        else if (IsPlayingAnimation("Hit3"))
        {
            playerController.isAttacking = true;
            playerController.attackValue = PlayerConstants.BASE_ATTACK_VALUE * PlayerConstants.HIT3_MULTIPLIER;
        }
        else
        {
            playerController.isAttacking = false;
            playerController.attackValue = PlayerConstants.BASE_ATTACK_VALUE * PlayerConstants.HIT1_MULTIPLIER;
        }
    }

    void OnClick()
    {
        playNextAnimation = true;
        lastClickedTime = Time.time;
    }

    private bool IsPlayingAnimation(string animationString)
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(animationString);
    }

    private bool IsComboAnimationFinished(string animationName)
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
               _animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    private bool IsIdle()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend");
    }
}