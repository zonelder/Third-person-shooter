using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLocomotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rigids = GetComponentsInChildren<Rigidbody>();
        foreach(var rigid in rigids)
        {
            rigid.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
