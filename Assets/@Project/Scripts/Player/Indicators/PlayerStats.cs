using SG;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Animator animator;

    public HealthBarPlayer healthBarPlayer;
    public StaminaBarPlayer staminaBarPlayer;

    private void Awake()
    {
        healthBarPlayer = FindObjectOfType<HealthBarPlayer>();
        staminaBarPlayer = FindObjectOfType<StaminaBarPlayer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBarPlayer.SetMaxHealth(maxHealth);
        healthBarPlayer.SetCurrentHealth(currentHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBarPlayer.SetMaxStamina(maxStamina);
        staminaBarPlayer.SetCurrentStamina(currentStamina);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private int SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBarPlayer.SetCurrentHealth(currentHealth);

        animator.Play("Damage");

        if (currentHealth <= 0)
        {
            Die();
            animator.Play("Death");
        }
    }

    public void TakeStaminaDamage(int damage)
    {
        currentStamina = currentStamina - damage; 
        staminaBarPlayer.SetCurrentStamina(currentStamina);

        if (currentStamina > maxStamina)
        {

        }
    }

    public void Die()
    {
        Collider playerCollider = GetComponentInChildren<Collider>();
        playerCollider.enabled = false;
    }
}
