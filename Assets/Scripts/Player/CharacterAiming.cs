using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;

// класс про все что угодня связанное со стрельбой :(
public class CharacterAiming : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private Cinemachine.AxisState xAxis;
    [SerializeField] private Cinemachine.AxisState yAxis;
    private void Start()
    {
        _mainCamera = Camera.main;
           // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {

    }
}
