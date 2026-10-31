
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField, Min(0f)] private float dps = 5f;

    // Deal damage continuously while touching the player
    private void OnTriggerStay2D(Collider2D other)
    {
        // Only damage the player
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Find the player's health component
        if (other.TryGetComponent<EntityHealth>(
            out EntityHealth playerHealth))
        {
            // Convert damage per second to damage per physics step
            float damage = dps * Time.fixedDeltaTime;

            playerHealth.LoseHealth(damage);
        }
    }
}
