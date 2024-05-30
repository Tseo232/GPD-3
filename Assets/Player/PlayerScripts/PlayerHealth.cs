using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private EnemyAi enemy; // Reference to the EnemyAi script
    private float regenerationDelay = 8f; // Delay before health regeneration starts
    private float regenerationRate = 5f; // Rate of health regeneration
    private float maxHealth = 100;
    private float currentHealth;
    private bool canRegenerate = false;
    private float regenerationTimer = 0f;
    public HealthBar healthBar;

    void Start()
    {
        enemy = FindObjectOfType<EnemyAi>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Check if health regeneration is needed
        if (currentHealth < maxHealth)
        {
            HealthRegen();
        }

        // Check if player's health is depleted
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void HealthRegen()
    {
        // Check if health can regenerate
        if (!canRegenerate)
        {
            regenerationTimer += Time.deltaTime;
            if (regenerationTimer >= regenerationDelay)
            {
                canRegenerate = true;
            }
        }
        else
        {
            // Regenerate health
            float amountToRegenerate = regenerationRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth + amountToRegenerate, maxHealth);
            healthBar.SetHealth(currentHealth); // Update health bar

            // Reset regeneration timer if health is fully regenerated
            if (currentHealth >= maxHealth)
            {
                canRegenerate = false;
                regenerationTimer = 0f;
            }
        }
    }

    // void TakeDamage()
    // {
    //     currentHealth -= enemy.EnemyDamage;
    //     healthBar.SetHealth(currentHealth); // Update health bar
    // }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            //TakeDamage();
        }
    }
}
