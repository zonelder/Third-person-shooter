using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private Ray _ray;
    RaycastHit _hitInfo;

    private void Start()
    {
        
    }

    private void Update()
    {
        _ray.origin = _mainCamera.transform.position;
        _ray.direction = _mainCamera.transform.forward;

        if(Physics.Raycast(_ray, out _hitInfo))
         transform.position = _hitInfo.point;
    }
}
