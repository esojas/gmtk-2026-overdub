using UnityEngine;

public class PlayerReplayObject : ReplayObject
{
    private Animator animator;

    public GameObject deathParticle;

    public Renderer cloneRenderer;

    private bool cloneDiedFromObstacle = false;

    private Rigidbody rb;

    private bool cloneIsDead = false;

    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int SpeedHash = Animator.StringToHash("Movement");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //deathParticle = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody>();
        rb.maxDepenetrationVelocity = 2f;
        //cloneRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        cloneDiedFromObstacle = false;
        cloneIsDead = false;
    }

    public override void SetDataForFrame(ReplayData data)
    {
        PlayerReplayData playerData = (PlayerReplayData)data;

        rb.MovePosition(playerData.position);

        animator.transform.rotation = playerData.playerRotation;

        cloneRenderer.enabled = playerData.isVisible;

        animator.SetBool(JumpHash, playerData.isJump);
        animator.SetBool(IsGroundedHash, playerData.isGrounded);
        Vector3 vel = playerData.movement;
        animator.SetFloat(SpeedHash, new Vector3(vel.x, 0, vel.z).magnitude);

        if ((playerData.deathThisFrame || cloneDiedFromObstacle) && !cloneIsDead)
        {
            cloneIsDead = true;

            Debug.LogWarning("Clone died");

            cloneRenderer.enabled = playerData.isVisible;

            Instantiate(deathParticle, this.transform.position, Quaternion.identity);

            Invoke("Death",.01f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            cloneDiedFromObstacle = true;
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
    
}
