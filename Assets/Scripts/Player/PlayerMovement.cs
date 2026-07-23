using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControl playerControlScript;
    Vector2 direction;
    Rigidbody rb;
    bool jumpPressed = false;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float distanceToGround;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerModel;

    private void OnEnable()
    {
        playerControlScript.OnMove += HandleDirection;
        playerControlScript.OnJumpPressed += JumpPressed;
    }

    private void OnDisable()
    {
        playerControlScript.OnMove -= HandleDirection;
        playerControlScript.OnJumpPressed -= JumpPressed;
    }

    private void Awake()
    {
        playerControlScript = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        Movement();
        HandleJump();
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
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
        Ray ray;
        ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, distanceToGround, layerToHit))
        {
            return true;
        }
        return false;
    }

    private void HandleJump()
    {
        if (jumpPressed && isGrounded())
        {
            //rb.linearVelocity = new Vector3(0, jumpForce, 0) * Time.deltaTime;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            Debug.Log("Jump is pressed!");
            jumpPressed = false;
        }
    }

    private void JumpPressed()
    {
        jumpPressed = true;
    }


}
