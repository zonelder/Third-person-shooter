using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HittableUnit : MonoBehaviour
{
    private Health _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
        var rigidBodyes = GetComponentsInChildren<Rigidbody>();     
        foreach(var rigidBody in rigidBodyes)
        {
             HitBox currentHitbox = rigidBody.gameObject.AddComponent<HitBox>();
            currentHitbox.Health = _health;
        }
    }

}
