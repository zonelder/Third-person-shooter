using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// класс про все что угодня связанное со стрельбой :(
public class CharacterAiming : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private float _aimDuration;
    [SerializeField] private KeyCode _aimKey;


    [Space(5)] [SerializeField] private RaycastWeapon _weapon;
    private void Start()
    {
        _mainCamera = Camera.main;
           // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
