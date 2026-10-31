
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float travelSpeed = 8f;
    [SerializeField] private float damage = 1f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D projectileRigidbody;

    // Initialize projectile movement after spawning
    public void InitializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }

    // Launch the projectile in the given direction
    private void Launch(Vector2 direction)
    {
        projectileRigidbody.linearVelocity =
            direction.normalized * travelSpeed;
    }

    // Detect collisions with other 2D colliders
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the projectile when it hits terrain
        if (other.CompareTag("Terrain"))
        {
            DestroyProjectile();
        }
    }

    // Remove the projectile from the scene
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
