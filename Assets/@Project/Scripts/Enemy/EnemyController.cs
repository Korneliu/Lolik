using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    private const string AnimAttack = "Attack", AnimWalk = "Walk", AnimDeath = "Death";
    [SerializeField] private float _health;
    [SerializeField] private float _dist;
    [SerializeField] private Animator _animator;
    [SerializeField] private HealthBar _helthBar;
    [SerializeField] private float _radius;
    [SerializeField] private int damage;

    private NavMeshAgent _navMeshAgent;
    private Entity.Player _player;

    private Entity.Player Player
    {
        get
        {
            if (_player == null)
            {
                var objectWithTag = GameObject.FindGameObjectWithTag("Player");
                _player = objectWithTag != null ? objectWithTag.GetComponent<Entity.Player>() : null;
            }

            return _player;
        }
    }


    private void Awake()
    {
        _helthBar.Init(_health);
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (Player == null) return;
        _dist = Vector3.Distance(Player.transform.position, transform.position);
        if (_dist < _radius && _dist > 1.3F)
        {
            _animator.SetBool(AnimWalk, true);
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(Player.transform.position);
        }
        else if (_dist < _radius && _dist < 1.3F)
        {
            _navMeshAgent.SetDestination(transform.position);
            _animator.SetBool(AnimWalk, false);
            if (Player != null) _animator.SetTrigger(AnimAttack);
        }
    }

    public void TakeDamage()
    {
        _helthBar.SetValue(--_health);
        if (_health == 0)
        {
            _animator.SetTrigger(AnimDeath);
            _animator.SetBool(AnimWalk, false);
            _dist = 0;
            _radius = 0;
        }
    }
}