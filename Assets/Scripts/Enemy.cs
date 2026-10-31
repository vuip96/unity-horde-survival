
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EntityHealth))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip enemyDeathSound;

    private EntityHealth entityHealth;
    private NavMeshAgent agent;
    private GameObject target;

    private void Awake()
    {
        // Get the required components
        entityHealth = GetComponent<EntityHealth>();
        agent = GetComponent<NavMeshAgent>();

        // Keep the enemy correctly oriented in our 2D game
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void OnEnable()
    {
        // Subscribe to the enemy's death event
        entityHealth.OnDeath += DestroyEnemy;
    }

    private void Start()
    {
        // Find the player using the Player tag
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Do nothing if the player is missing
        if (target == null)
            return;

        // Only navigate when the enemy is on a valid NavMesh
        if (agent.isOnNavMesh)
        {
            // Continuously update the destination
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the death event
        if (entityHealth != null)
        {
            entityHealth.OnDeath -= DestroyEnemy;
        }
    }

    public void DestroyEnemy()
    {
        // Play the enemy death sound
        if (enemyDeathSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAudio(
                enemyDeathSound,
                SoundType.SFX,
                0.7f,
                false
            );
        }

        // Remove the enemy from the scene
        Destroy(gameObject);
    }
}
