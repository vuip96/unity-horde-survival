
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private EntityHealth playerHealth;

    private void Awake()
    {
        // Start with a full health bar
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = 1f;
        }
    }

    private void OnEnable()
    {
        // Listen for changes in player health
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += OnHealthChanged;
        }
    }

    private void OnDisable()
    {
        // Stop listening when this component is disabled
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if (healthBarFill == null || maxHealth <= 0f)
            return;

        // Convert current health to a value between 0 and 1
        healthBarFill.fillAmount =
            Mathf.Clamp01(currentHealth / maxHealth);
    }
}
