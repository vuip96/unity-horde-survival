
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

    [Header("Audio")]
    [SerializeField] private AudioClip enemyHitSound;

    private bool isDestroyed;

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

    // Detect collisions with enemies and terrain
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed)
            return;

        // Damage enemies
        if (other.CompareTag("Enemy"))
        {
            DealDamage(other.gameObject);
            DestroyProjectile();
        }
        // Destroy the projectile when hitting a wall
        else if (other.CompareTag("Terrain"))
        {
            DestroyProjectile();
        }
    }

    // Apply damage to an entity with health
    private void DealDamage(GameObject target)
    {
        if (target.TryGetComponent<EntityHealth>(
            out EntityHealth health))
        {
            health.LoseHealth(damage);

            // Play the enemy hit sound
            if (enemyHitSound != null &&
                AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayAudio(
                    enemyHitSound,
                    SoundType.SFX,
                    0.5f,
                    false
                );
            }
        }
    }

    // Spawn hit particles and remove the projectile
    private void DestroyProjectile()
    {
        if (isDestroyed)
            return;

        isDestroyed = true;

        // Spawn the hit particle effect
        if (hitParticles != null)
        {
            ParticleSystem particles = Instantiate(
                hitParticles,
                transform.position,
                Quaternion.identity
            );

            Destroy(particles.gameObject, 1.5f);
        }

        Destroy(gameObject);
    }
}
