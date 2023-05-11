using SG;
using UnityEngine;

public class HelperEnemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;

    public void TakeDamage(int damage) => enemyStats.TakeDamage(damage);
}
