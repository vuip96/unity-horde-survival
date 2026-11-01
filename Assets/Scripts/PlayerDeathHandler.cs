
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDeathHandler : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerStaffController staffController;

    [Header("Death Settings")]
    [SerializeField] private float deathAnimationDelay = 0.6f;

    private EntityHealth entityHealth;
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        // Get the required components
        entityHealth = GetComponent<EntityHealth>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Listen for the player's death event
        entityHealth.OnDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        // Stop listening when disabled
        if (entityHealth != null)
        {
            entityHealth.OnDeath -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        // Stop player movement
        playerRigidbody.linearVelocity = Vector2.zero;

        if (playerController != null)
            playerController.enabled = false;

        // Disable shooting
        if (staffController != null)
            staffController.enabled = false;

        // Play the death animation
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isDead", true);
        }

        // Show Game Over after the animation
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(deathAnimationDelay);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
