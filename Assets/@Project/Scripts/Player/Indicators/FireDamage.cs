using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damage = 1;
    public float radius = 2f;
    public LayerMask layerMask;

    public float timer = 0.1f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            foreach (Collider collider in colliders)
            {
                collider.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
