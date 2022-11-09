using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnDie;
    public Action OnGetDamage;
    public Action OnRevive;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Ragdoll _ragdoll;
    private bool _isAlive;

    private void Start()
    {
        if(_currentHealth>0)
        {
            _isAlive = true;
        }
    }


    public void GetDamage(float amount)
    {
        if (!_isAlive)
            return;
        _currentHealth -= amount;
        OnGetDamage?.Invoke();
        if(_currentHealth<=0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
        _isAlive = false;
        _ragdoll.ActivateRagdoll();
        
    }

    public void Revive()
    {

        _isAlive = true;
        _currentHealth = _maxHealth;
        _ragdoll.DeactivateRagdoll();
        OnRevive?.Invoke();
    }
}
