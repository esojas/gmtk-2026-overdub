using UnityEngine;

public class PlayerReplayObject : ReplayObject
{
    private Animator animator;

    public GameObject deathParticle;

    private Renderer cloneRenderer;

    private bool cloneDiedFromObstacle = false;

    private Rigidbody rb;

    private bool cloneIsDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //deathParticle = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody>();
        cloneRenderer = GetComponent<Renderer>();
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
        rb.MoveRotation(playerData.playerRotation);

        cloneRenderer.enabled = playerData.isVisible;

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
