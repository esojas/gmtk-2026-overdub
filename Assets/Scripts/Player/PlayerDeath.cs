using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Transform respawnPos;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private float playerLifetime;
    public float timeRemaining { get; private set; }
    private Rigidbody rb;
    private Collider playerCollider;
    private Renderer playerRenderer;
    private PlayerMovement playerMovement;
    private Recorder recorder;

    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // 3 means harmfullobstacles
        {
            HandleDeath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Catches solid objects without "Is Trigger" checked
        if (collision.gameObject.layer == 3)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (isDead)
        {
            return;
        }

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



    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForEndOfFrame();

        Respawn();

        //recorder.StartNewRecording();
    }

    

    private void Respawn()
    {
        isDead = false;

        rb.position = respawnPos.position;

        rb.linearVelocity = Vector3.zero;

        this.transform.position = respawnPos.position;

        rb.useGravity = true;
        playerCollider.enabled = true;
        playerRenderer.enabled = true;

        StartRecordingObject.startRecording = false;
        StartRecordingObject.hasCollided = false;
        foreach (MovableObtacle obstacle in MovableObtacle.AllObstacles) // moves all movable object back at their spawn
        {
            obstacle.ResetObjectToOrigin();
        }

        timeRemaining = playerLifetime;

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

        timeRemaining = playerLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartRecordingObject.startRecording && !isDead)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                Debug.LogWarning(timeRemaining);
                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    HandleDeath();
                }
            }
        }
    }
}
