using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Transform respawnPos;
    private Rigidbody rb;
    private Collider playerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) // 3 means harmfullobstacles
        {
            Debug.Log("death");

            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            playerCollider.enabled = false;
            // do something to hide player?
            // play death particle
            Respawn();
            
        }
    }

    private void Respawn()
    {
        //PausedControl.Instance.TogglePause();
        //SceneManager.LoadScene("Level1"); // currentlevel
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
