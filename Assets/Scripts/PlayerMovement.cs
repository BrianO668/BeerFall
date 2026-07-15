using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX = -8.5f;
    public float maxX = 8.5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        bool isWalking = false;

        if (Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.aKey.isPressed)
        {
            movement = Vector2.left;
            isWalking = true;
            spriteRenderer.flipX = true;
        }
        else if (Keyboard.current.rightArrowKey.isPressed ||
                 Keyboard.current.dKey.isPressed)
        {
            movement = Vector2.right;
            isWalking = true;
            spriteRenderer.flipX = false;
        }

        animator.SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        rb.MovePosition(newPosition);
    }
}
