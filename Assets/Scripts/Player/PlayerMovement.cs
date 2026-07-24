using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControl playerControlScript;
    private Recorder recorder;
    Vector2 direction;
    Rigidbody rb;
    Ray ray;
    bool jumpPressed = false;
    private float jumpCooldownTimerValue = 0f;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float distanceToGround;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerModel;
    [SerializeField] private float jumpCooldownTimer;
    [Header("Collision")]
    [SerializeField] private Transform groundCheck; 
    


    private void OnDisable()
    {
        playerControlScript.OnMove -= HandleDirection;
        playerControlScript.OnJumpPressed -= JumpPressed;
    }

    private void Awake()
    {
        playerControlScript = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody>();
        recorder = GetComponent<Recorder>();
    }

    void Start()
    {

        playerControlScript.OnMove += HandleDirection;
        playerControlScript.OnJumpPressed += JumpPressed;
    }

    private void Update()
    {
        if (jumpCooldownTimerValue > 0) jumpCooldownTimerValue -= Time.deltaTime;
        Debug.DrawRay(groundCheck.position, Vector3.down * distanceToGround, Color.red);
    }

    private void LateUpdate()
    {
        ReplayData data = new ReplayData(this.transform.position);

        recorder.RecordReplayFrame(data);
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void HandleDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void Movement()
    {
        Transform cam_Transform = cam.transform;

        Vector3 camForward = cam_Transform.forward;
        Vector3 camRight = cam_Transform.right;


        Vector3 movement = (camForward * direction.y + camRight * direction.x);

        movement = Vector3.ClampMagnitude(movement, 1);

        Quaternion targetRotation = Quaternion.Euler(
        0,                              // X always 0, never tilt
        cam_Transform.eulerAngles.y,    // Y follows camera horizontal
        0                               // Z always 0
        );

        playerModel.rotation = Quaternion.Slerp(
             playerModel.rotation,
             targetRotation,
             10f * Time.deltaTime
         );

        rb.linearVelocity = new Vector3(movement.x * movementSpeed, rb.linearVelocity.y, movement.z * movementSpeed);
    }

    private bool isGrounded()
    {
        Ray groundRay = new Ray(groundCheck.position, Vector3.down);
        return Physics.Raycast(groundRay, distanceToGround, layerToHit);
    }

    private void HandleJump()
    {
        if ((jumpPressed && isGrounded()))
        {
            //rb.linearVelocity = new Vector3(0, jumpForce, 0) * Time.deltaTime;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            Debug.Log("Jump is pressed!");
            jumpPressed = false;
            jumpCooldownTimerValue = jumpCooldownTimer;
        }
    }

    private void JumpPressed()
    {
        if (jumpCooldownTimerValue > 0) return;
        jumpPressed = true;
        HandleJump();
    }


}
