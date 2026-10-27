
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Reference to the player's Rigidbody2D component.
    private Rigidbody2D _rb;

    // Player movement speed, adjustable in the Unity Inspector.
    public float movementSpeed = 5f;

    void Start()
    {
        // Get the Rigidbody2D attached to this GameObject.
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle player movement every frame.
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        // Read horizontal and vertical movement input.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Combine input values into a 2D movement vector.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Prevent faster diagonal movement.
        movement = Vector2.ClampMagnitude(movement, 1f);

        // Apply movement speed to the Rigidbody2D.
        _rb.linearVelocity = movement * movementSpeed;
    }
}
