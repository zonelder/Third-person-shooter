using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGrapth : MonoBehaviour
{
    [SerializeField] private AnimationCurve _PositionCurve;
    void Start()
    {
        _PositionCurve = new AnimationCurve();
    }

    // Update is called once per frame
    void Update()
    {
        _PositionCurve.AddKey(Time.time, transform.position.z);
    }
}
