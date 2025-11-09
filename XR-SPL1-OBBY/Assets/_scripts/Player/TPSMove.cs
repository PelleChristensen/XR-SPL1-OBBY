using UnityEngine;
using UnityEngine.InputSystem;

public class TPSMove : MonoBehaviour
{
    public Transform character;
    public Rigidbody rb;
    public InputActionReference moveaction;
    public InputActionReference jumpaction;
    public InputActionReference sprintaction;
    public Animator animator; 

    [Header("Movement Settings")]
    public float moveForce = 10f;
    public ForceMode forceMode = ForceMode.Force;
    public bool useOrientationForward = true;
    public float jumpPower = 5f;

    private bool isGrounded;
    public bool playerCanMove;
    private bool isWalking = true;
    public float fallTreshold = 1.2f;
    private float fallTime = 0f; 
    private float groundCheckDistance = 0.2f;
    public LayerMask groundMask; 


    void Awake()
    {
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (!character) character = transform;

        if (moveaction && moveaction.action != null)
        {
            moveaction.action.Enable();
        }
        if (jumpaction && jumpaction.action != null)
        {
            jumpaction.action.Enable();
            jumpaction.action.performed += Jump;
        }
    }

    void OnEnable()
    {
        if (moveaction && moveaction.action != null)
        {
            moveaction.action.Enable();
        }
        if (jumpaction && jumpaction.action != null)
        {
            jumpaction.action.Enable();
            jumpaction.action.performed += Jump;
        }
    }

    void OnDisable()
    {
        if (moveaction && moveaction.action != null)
        {
            moveaction.action.Disable();
        }
        if (jumpaction && jumpaction.action != null)
        {
            jumpaction.action.Disable();
            jumpaction.action.performed -= Jump;
        }
    }

    void FixedUpdate()
    {

        Vector2 moveInput = moveaction && moveaction.action != null
            ? moveaction.action.ReadValue<Vector2>()
            : Vector2.zero;

        // Convert to 3D direction
        Vector3 moveDir;
        if (useOrientationForward && character != null)
        {
            Vector3 forward = character.forward; forward.y = 0f; forward.Normalize();
            Vector3 right = character.right; right.y = 0f; right.Normalize();
            moveDir = forward * moveInput.y + right * moveInput.x;
        }
        else
        {
            moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        }

        // Apply force
        if (moveDir.sqrMagnitude > 0.001f)
        {
            rb.AddForce(moveDir * moveForce, forceMode);
        }
        
        if (!IsGrounded())
        {
            // Player is in the air
            fallTime += Time.deltaTime;

            if (fallTime > fallTreshold)
            {
                //animator.SetBool("isFalling", true);
            }
        }
        else
        {
            // Player is on the ground, reset timer
            fallTime = 0f;
            //animator.SetBool("isFalling", false);
        }

    }

    private void Jump(InputAction.CallbackContext context)
    {
        // Adds force to the player rigidbody to jump
        if (IsGrounded())
        {
            // Debug.Log("User jumps: IsGrounded");
            rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }
}
