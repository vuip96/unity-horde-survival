
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement
    private Rigidbody2D _rb;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5f;

    // Player animations and visuals
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _characterBody;

    // Footstep audio settings
    [Header("Footstep Audio")]
    [SerializeField] private AudioClip footstepAudio;
    [SerializeField, Range(0f, 1f)] private float footstepVolume = 0.7f;
    [SerializeField, Min(0.05f)] private float footstepInterval = 0.14f;

    private float nextFootstepAudio;

    private void Start()
    {
        // Get the Rigidbody2D attached to the Player
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Update movement, animations and footstep sounds
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        // Read movement input from the keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a 2D movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Prevent faster diagonal movement
        movement = Vector2.ClampMagnitude(movement, 1f);

        // Move the player using Rigidbody2D
        _rb.linearVelocity = movement * _movementSpeed;

        // Check whether the player is walking
        bool characterIsWalking = movement.magnitude > 0f;

        // Update the walking animation
        _animator.SetBool("isWalking", characterIsWalking);

        // Flip the character when moving left or right
        if (movement.x != 0f)
        {
            _characterBody.flipX = movement.x < 0f;
        }

        // Play footsteps only while walking
        if (characterIsWalking)
        {
            HandleWalkingSounds();
        }
        else
        {
            // Allow an immediate footstep when walking resumes
            nextFootstepAudio = Time.time;
        }
    }

    private void HandleWalkingSounds()
    {
        // Wait until the next footstep can be played
        if (Time.time < nextFootstepAudio)
        {
            return;
        }

        // Play the footstep through the SFX mixer group
        if (footstepAudio != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAudio(
                footstepAudio,
                SoundType.SFX,
                footstepVolume,
                false
            );
        }

        // Schedule the next footstep sound
        nextFootstepAudio = Time.time + footstepInterval;
    }
}
