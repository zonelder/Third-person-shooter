using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// класс про все что угодня связанное со стрельбой :(
public class CharacterAiming : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private float _aimDuration;
    [SerializeField] private Rig _aimLayer;
    [SerializeField] private KeyCode _aimKey;
    [SerializeField] private KeyCode _fireKey; 

    [Space(5)] [SerializeField] private RaycastWeapon _weapon;
    private void Start()
    {
        _mainCamera = Camera.main;
           // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if(_aimLayer)
        {
            _aimLayer.weight = 1;
        }

        if (_weapon)
        {
            if (Input.GetKeyDown(_fireKey))
            {
                _weapon.StartFiring();
            }
            if (Input.GetKeyUp(_fireKey))
            {
                _weapon.StopFiring();
            }
        }

    }
}
