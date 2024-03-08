using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;
    public float dashDuration;
    private Animator playerAnimator;
    public float dashCooldown;
    public CharacterController controller;
    public Vector3 moveDirection;
    public float gravityScale;
    public float coyoteTime; // 5/60 of a second by default
    public float coyoteTimeCounter;
    public float jumpBufferTime; // 6/60 of a second by default
    public float jumpBufferCounter;
    public float invincibilityTime; // 20/60 of a second by default
    public float invincibilityTimeCounter;
    public int maxHealth; // 5 by default
    public int health;

    public AudioClip dashSound;
    

    [SerializeField] private TrailRenderer tr;

    public Transform pivot;

    private bool isDashing;
    private bool canDash = true;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        tr.enabled = false;
        health = maxHealth;
        Time.timeScale = 1f;
    }

    void Update()
    {
        HandleMovementInput();
        ApplyGravity();
        HandleJumpInput();
        
        // Update i-frame time
        if (invincibilityTimeCounter >= 0f)
        {
            invincibilityTimeCounter -= Time.deltaTime;
        }

        // Handle dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);

        // allows for player direction based on camera direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
        }

        // checks if player model is grounded, if grounded then sets false, if not grounded then sets true and plays jumping animation
        playerAnimator.SetBool("isGrounded", controller.isGrounded);

        // checks player model's speed, if greater than 0.1 then plays moving animation, if not plays idle animation
        playerAnimator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));

        // checks if player model is able to dash, if so trigger animation
        if (isDashing && canDash)
        {
            playerAnimator.SetTrigger("Dashing");
        }
    }

    // returns true when the player is successfully hit
    public bool HandleDamage(int damage)
    {
        // If not in i-frames, hit the player and give them i-frames
        if (invincibilityTimeCounter < 0f)
        {
            health -= damage;
            invincibilityTimeCounter = invincibilityTime;
            return true;
        }
        return false;
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
        if (controller.isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            coyoteTimeCounter = Mathf.Max(coyoteTimeCounter, -10f); // prevents underflow
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
            jumpBufferCounter = Mathf.Max(jumpBufferCounter, -10f); //prevents underflow

        }
        // Allows the player to jump slightly after and before they touch the ground
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            moveDirection.y = jumpForce;
            playerAnimator.SetBool("isGrounded", false);
            jumpBufferCounter = 0f;
        }
        // Player jumps lower depending on how long they hold jump
        if (Input.GetButtonUp("Jump") && moveDirection.y > 0f)
        {
            moveDirection.y *= 0.5f;
            coyoteTimeCounter = 0f;
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = -0.5f;
        }
        moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        // Enable the TrailRenderer
        tr.enabled = true;
        AudioSource.PlayClipAtPoint(dashSound, transform.position);

        // Trigger the dash animation
        playerAnimator.SetTrigger("Dashing");

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
