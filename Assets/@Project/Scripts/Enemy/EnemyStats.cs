using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace SG
{
    public class EnemyStats : MonoBehaviour
    {
        private const string AnimAttack = "Attack", AnimWalk = "Walk", AnimIdle = "Idle";

        Animator animator;
        DamageCollider damageCollider;

        public int soulsAwardedOnDeath = 100;

        public Transform target;
        public float distanceToFollow = 10f;
        public float distanceToStop = 1f;
        public float distanceToStopFollowing = 15f;
        public Vector3 startingPosition;
        private NavMeshAgent navMeshAgent;

        public int currentHealth;
        public int maxHealth;
        public int healthLevel = 10;

        public GameObject enemy;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            startingPosition = transform.position;

            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;

            maxHealth = enemy.GetComponent<EnemyStats>().maxHealth;
        }

        void Update()
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (target != null && distanceToTarget <= distanceToFollow)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - direction * distanceToStop;
                transform.LookAt(target);
                animator.SetBool(AnimWalk, true);

                if (Vector3.Distance(transform.position, targetPosition) > distanceToStop)
                {
                    navMeshAgent.SetDestination(targetPosition);
                }
                else
                {
                    animator.SetBool(AnimWalk, false);
                    animator.SetBool(AnimAttack, true);
                    navMeshAgent.SetDestination(transform.position);
                }
            }

            if (damageCollider == true)
            {
                
                currentHealth = enemy.GetComponent<EnemyStats>().currentHealth;
            }  
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            animator.Play("Damage");

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public void Die()
        {
            Collider enemyCollider = GetComponentInChildren<Collider>();
            enemyCollider.enabled = false;
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            Die();
            animator.Play("Death");
            distanceToFollow = 0f;
        }

        public void AwardSoulsOnDeath()
        {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

            if (playerStats != null)
            {
                playerStats.AddSouls(soulsAwardedOnDeath);

                if (soulCountBar != null)
                {
                    soulCountBar.SetSoulCountText(playerStats.soulCount);
                }
            }
        }

    }
}

