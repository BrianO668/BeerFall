using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX = -8.5f;
    public float maxX = 8.5f;
    public float jumpForce;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public float moveSpeedMultiplier = 1f;

    private float moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Player awake");
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = 0f;

        //Left-right movement
        if (Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.aKey.isPressed)
        {
            moveInput = -1f;
            spriteRenderer.flipX = true;
        }
        else if (Keyboard.current.rightArrowKey.isPressed ||
                 Keyboard.current.dKey.isPressed)
        {
            moveInput = 1f;
            spriteRenderer.flipX = false;
        }

        //Jumping
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetBool("isWalking", moveInput != 0);
    }

    public void ChangeMoveSpeedMultiplier(float newValue)
    {
        moveSpeedMultiplier = newValue;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed * moveSpeedMultiplier, rb.linearVelocity.y);

        Vector2 position = rb.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        rb.position = position;

        //Ground checking
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
    }
}
