
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Reference to the player's Rigidbody2D component.
    private Rigidbody2D _rb;

    // Movement speed, adjustable in the Unity Inspector.
    [SerializeField] private float _movementSpeed = 5f;

    // Reference to the player's Animator component.
    [SerializeField] private Animator _animator;

    // Reference to the player's SpriteRenderer component.
    [SerializeField] private SpriteRenderer _characterBody;

    void Start()
    {
        // Get the Rigidbody2D attached to the Player.
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Update player movement and animations every frame.
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        // Read movement input from the keyboard.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a 2D movement vector.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Prevent faster diagonal movement.
        movement = Vector2.ClampMagnitude(movement, 1f);

        // Move the player using Rigidbody2D.
        _rb.linearVelocity = movement * _movementSpeed;

        // Check whether the player is moving.
        bool characterIsWalking = movement.magnitude > 0f;

        // Update the walking animation.
        _animator.SetBool("isWalking", characterIsWalking);

        // Flip the character when moving left or right.
        if (movement.x != 0f)
        {
            _characterBody.flipX = movement.x < 0f;
        }
    }
}
