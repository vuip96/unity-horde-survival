
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float travelSpeed = 8f;
    [SerializeField] private float damage = 1f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D projectileRigidbody;

    [Header("Hit Effects")]
    [SerializeField] private ParticleSystem hitParticles;

    // Initialize projectile movement
    public void InitializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }

    // Launch the projectile towards the target
    private void Launch(Vector2 direction)
    {
        projectileRigidbody.linearVelocity =
            direction.normalized * travelSpeed;
    }

    // Detect collisions with terrain
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Terrain"))
        {
            DestroyProjectile();
        }
    }

    // Create the hit effect and destroy the projectile
    private void DestroyProjectile()
    {
        // Spawn particles at the projectile's position
        if (hitParticles != null)
        {
            ParticleSystem particles = Instantiate(
                hitParticles,
                transform.position,
                Quaternion.identity
            );

            // Remove the particles after the effect finishes
            Destroy(particles.gameObject, 1.5f);
        }

        // Remove the projectile
        Destroy(gameObject);
    }
}
