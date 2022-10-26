using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGraph : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _curve.AddKey(Time.time, transform.eulerAngles.y);
    }
}
