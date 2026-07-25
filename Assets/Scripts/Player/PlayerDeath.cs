using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Transform respawnPos;
    [SerializeField] private GameObject deathParticle;
    private Rigidbody rb;
    private Collider playerCollider;
    private Renderer playerRenderer;
    private PlayerMovement playerMovement;
    private Recorder recorder;

    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }

        if (other.gameObject.layer == 3) // 3 means harmfullobstacles
        {
            Debug.Log("death");
            isDead = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            playerCollider.enabled = false;
            playerRenderer.enabled = false;
            // do something to hide player?

            Debug.Log(deathParticle == null ? "NULL REF" : "Ref OK, playing");

            Instantiate(deathParticle, this.transform.position, Quaternion.identity);

            playerMovement.deathThisFrame = true;

            StartCoroutine(WaitAndRespawn());

        }
    }

    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForEndOfFrame();

        Respawn();

        //recorder.StartNewRecording();
    }

    private void Respawn()
    {
        isDead = false;
        rb.useGravity = true;
        playerCollider.enabled = enabled;
        playerRenderer.enabled = true;
        this.transform.position = respawnPos.position;

        StartRecordingObject.startRecording = false;
        //GameEventsManager.Instance.GoalReached();

        //PausedControl.Instance.TogglePause();
        //SceneManager.LoadScene("Level1"); // currentlevel
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerMovement = GetComponent<PlayerMovement>();
        playerRenderer = GetComponent<Renderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recorder = GetComponent<Recorder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
