using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;
    public float dashDuration;
    public float dashCooldown;
    public CharacterController controller;
    public Vector3 moveDirection;
    public float gravityScale;

    [SerializeField] private TrailRenderer tr;

    private bool isDashing;
    private bool canDash = true;

    private void Start()
    {
        tr.enabled = false;
    }

    void Update()
    {
        HandleMovementInput();

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            HandleJumpInput();
        }

        ApplyGravity();


        // Handle dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleMovementInput()
    {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        Vector3 forwardComponent = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 horizComponent = transform.right * Input.GetAxisRaw("Horizontal");
        float vertComponent = moveDirection.y;
        moveDirection = moveSpeed * (forwardComponent + horizComponent).normalized;
        moveDirection.y = vertComponent;
    }

    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }
    }

    void ApplyGravity()
    {
        moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        // Enable the TrailRenderer
        tr.enabled = true;

        // Store the original move speed
        float originalMoveSpeed = moveSpeed;

        // Increase move speed for the dash duration
        moveSpeed += dashForce;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Reset the move speed
        moveSpeed = originalMoveSpeed;

        // Disable the TrailRenderer
        tr.enabled = false;

        // Allow dashing again after cooldown
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        isDashing = false;
    }
}
