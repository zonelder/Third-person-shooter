using UnityEngine;
using Cinemachine;
using Zenject;
using UnityEngine.Animations.Rigging;

public class CameraSettingConstructor : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private CinemachineFreeLook _cinemachineCamera;

    [Inject]
    public void Construct(CharacterLocomotion Player)
    {

         _cinemachineCamera.Follow = Player.transform;
         _cinemachineCamera.LookAt = Player.transform.Find("CameraLookAt").transform;
        Player.GetComponent<ActiveWeapon>().Construct(_cinemachineCamera);
    }

}
