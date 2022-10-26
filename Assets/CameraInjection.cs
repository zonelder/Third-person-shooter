using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Zenject;
public class CameraInjection : MonoBehaviour
{

    [SerializeField] private CinemachineFreeLook _cinemachineCamera;

    [Inject]
    public void Construct(CharacterLocomotion Player)
    {
        _cinemachineCamera.Follow = Player.transform;
        _cinemachineCamera.LookAt = Player.transform.Find("CameraLookAt").transform;
    }
}
