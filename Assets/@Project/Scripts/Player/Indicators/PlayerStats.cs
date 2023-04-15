using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int healthLevel = 10;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    [SerializeField] int staminaLevel = 10;
    [SerializeField] int maxStamina;
    [SerializeField] int currentStamina;


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
}
