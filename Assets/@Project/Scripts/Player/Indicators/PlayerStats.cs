using SG;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    PlayerInput playerInput;
    AnimatorManager animatorManager;

    [SerializeField] float rollForward = 1f;

    private Animator animator;

    public HealthBarPlayer healthBarPlayer;
    public StaminaBarPlayer staminaBarPlayer;

    public float staminaRegenerationAmount = 10f;
    public float timer = 0f;
    public float recoveryTime = 1f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
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

    public void Roll()
    {
        transform.Translate(Vector3.forward * rollForward * Time.deltaTime);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private float SetMaxStaminaFromStaminaLevel()
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
        if (playerInput.isInvulnerable)
            return;

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

    public void RegenerateStamina()
    {
        if (currentStamina < maxStamina && timer < recoveryTime)
        {
            timer += Time.deltaTime;
        }
        else if (currentStamina < maxStamina && timer >= recoveryTime)
        {
            currentStamina += staminaRegenerationAmount * Time.deltaTime;
            staminaBarPlayer.SetCurrentStamina(Mathf.RoundToInt(currentStamina));

            if (currentStamina >= maxStamina)
            {
                timer = 0f;
            }
        }
    }

}
