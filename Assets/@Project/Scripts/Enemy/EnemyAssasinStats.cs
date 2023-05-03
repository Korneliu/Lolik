using SharpNav.Crowds;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyAssasinStats : CharacterStats
    {
        private const string AnimAttack = "Attack", AnimWalk = "Walk", AnimIdle = "Idle";

        Animator animator;

        public Transform target; // ÷ель дл€ следовани€ (игрок)
        public float distanceToFollow = 10f; // –ассто€ние на котором враг начнет следовать за игроком
        public float distanceToStop = 1f; // –ассто€ние, при котором враг остановитс€ перед игроком
        public float distanceToStopFollowing = 15f; // –ассто€ние, при достижении которого враг вернетс€ на свою позицию
        public Vector3 startingPosition; // Ќачальна€ позици€ врага
        private NavMeshAgent navMeshAgent; // NavMeshAgent дл€ перемещени€

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
                currentHealth = 0;
                Die();
                animator.Play("Death");
                distanceToFollow = 0f;
            }
        }

        public void Die()
        {
            Collider enemyCollider = GetComponentInChildren<Collider>();
            enemyCollider.enabled = false;
        }
    }
}

