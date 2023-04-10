using Controller.Movement;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int healthLevel = 10;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    private Animator animator;

    public HealthBarPlayer healthBarPlayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBarPlayer.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        animator.Play("Damage");
        currentHealth = currentHealth - damage;
        healthBarPlayer.SetCurrentHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Death");
        }
    }
}
