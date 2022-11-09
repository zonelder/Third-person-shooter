using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFrezeer : MonoBehaviour
{

    [SerializeField] private bool _x;
    [SerializeField] private bool _y;
    [SerializeField] private bool _z;
    private Quaternion _requeredRotation;

    // Start is called before the first frame update
    void Awake()
    {
        _requeredRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        _requeredRotation = Quaternion.Euler((_x) ? (_requeredRotation.eulerAngles.x) : transform.rotation.eulerAngles.x,
                                              (_y) ? (_requeredRotation.eulerAngles.y) : transform.rotation.eulerAngles.y,
                                                (_z) ? (_requeredRotation.eulerAngles.z) : transform.rotation.eulerAngles.z);
        transform.rotation = _requeredRotation;
    }
}
