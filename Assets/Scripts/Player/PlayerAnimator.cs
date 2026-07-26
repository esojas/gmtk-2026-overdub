using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");
    private static readonly int MovementHash = Animator.StringToHash("Movement");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerMovement.OnJumpExecuted += HandleJumpTriggered;
    }

    private void OnDestroy()
    {
        if (playerMovement != null)
            playerMovement.OnJumpExecuted -= HandleJumpTriggered;
    }

    void Update()
    {
        animator.SetBool(IsGroundedHash, playerMovement.IsGrounded);
        animator.SetFloat(MovementHash, playerMovement.PlanarSpeed);
    }

    private void HandleJumpTriggered()
    {
        animator.SetTrigger(JumpHash);
    }
}
