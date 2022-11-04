using UnityEngine;
using Zenject;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]
public class FollowerAI : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _agent;
    private Animator _animator;

    [Inject]
    public void Construct(CharacterLocomotion player)
    {
        _player = player.gameObject;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        var health = GetComponent<Health>();
        health.OnDie += StopAI;
        health.OnRevive += StartAi;
    }


    private void Ondisable()
    {
        var health = GetComponent<Health>();
        health.OnDie -= StopAI;
        health.OnRevive -= StartAi;
    }

    void Update()
    {
        if(_agent.enabled)
        {
            _agent.SetDestination(_player.transform.position);
            _animator.SetFloat("InputY", _agent.velocity.magnitude / _agent.speed);
        }

    }


    private void StopAI()
    {
        _agent.enabled = false;
    }
    private void StartAi()
    {
        _agent.enabled = true;
    }
}
