
using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField, Min(1f)] private float maxHealth = 10f;
    [SerializeField, Min(0f)] private float healthRegen = 0f;

    [SerializeField] private float currentHealth;
    private bool isDead;

    // Events for health changes and death
    public event Action OnDeath;
    public event Action<float, float> OnHealthChanged;

    private void Awake()
    {
        // Initialize health
        currentHealth = maxHealth;
        isDead = false;
    }

    private void Start()
    {
        // Regenerate health once per second
        InvokeRepeating(nameof(HandleHealthRegen), 1f, 1f);
    }

    // Reduce health by the given damage amount
    public void LoseHealth(float damage)
    {
        if (isDead || damage <= 0f)
            return;

        currentHealth = Mathf.Clamp(
            currentHealth - damage, 0f, maxHealth
        );

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Death();
        }
    }

    // Notify listeners when the entity dies
    private void Death()
    {
        if (isDead)
            return;

        isDead = true;
        CancelInvoke(nameof(HandleHealthRegen));

        OnDeath?.Invoke();
    }

    // Restore health over time
    private void HandleHealthRegen()
    {
        if (isDead || healthRegen <= 0f || currentHealth >= maxHealth)
            return;

        currentHealth = Mathf.Clamp(
            currentHealth + healthRegen, 0f, maxHealth
        );

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
