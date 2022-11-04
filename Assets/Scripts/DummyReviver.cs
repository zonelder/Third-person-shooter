using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DummyReviver : MonoBehaviour
{
    private Health _health;
    [SerializeField] private float _revivingDelay;
    private float _timeLeft;
    private bool _isCounting;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }


    public void PrepareToRevive()
    {
        _timeLeft = _revivingDelay;
        _isCounting = true;
    }
    // Update is called once per frame
    private void Update()
    {
        if(_isCounting)
        {
            _timeLeft -= Time.deltaTime;
            if(_timeLeft<=0)
            {
                _isCounting = false;
                _health.Revive();
            }
        }
        
    }



    private void OnEnable()
    {
        _health.OnDie += PrepareToRevive;
    }
    private void OnDisable()
    {
        _health.OnDie -= PrepareToRevive;
    }
}
