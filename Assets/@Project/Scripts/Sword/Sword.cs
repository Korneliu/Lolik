using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sword : MonoBehaviour, IDamageable
{
    private List<IDamageable> _damageables = new();

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable component))
            _damageables.Add(component);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable component))
            _damageables.Remove(component);
    }

    public void TakeDamage()
    {
        foreach (var damageable in _damageables)
            damageable.TakeDamage();
    }
}