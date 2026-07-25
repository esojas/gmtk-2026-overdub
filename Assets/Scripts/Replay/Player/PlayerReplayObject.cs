using UnityEngine;

public class PlayerReplayObject : ReplayObject
{
    private Animator animator;

    public GameObject deathParticle;

    private Renderer cloneRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //deathParticle = GetComponent<GameObject>();

        cloneRenderer = GetComponent<Renderer>();
    }

    public override void SetDataForFrame(ReplayData data)
    {
        PlayerReplayData playerData = (PlayerReplayData)data;

        this.transform.position = playerData.position;

        cloneRenderer.enabled = playerData.isVisible;

        if (playerData.deatThisFrame)
        {
            Debug.LogWarning("Clone died");

            cloneRenderer.enabled = playerData.isVisible;

            Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        }
    }

    
}
