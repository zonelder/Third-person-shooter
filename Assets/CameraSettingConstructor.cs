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
        /*_mainCamera = Camera.main;
       // var Cinemachine = Instantiate(_cinemachineCamera, null);
        RigBuilder rigBuilder = Player.GetComponent<RigBuilder>();
        // Cinemachine.Follow = Player.transform;
        // Cinemachine.LookAt = Player.transform.Find("CameraLookAt").transform;
         _cinemachineCamera.Follow = Player.transform;
         _cinemachineCamera.LookAt = Player.transform.Find("CameraLookAt").transform;
        Transform cameraLookAt = _mainCamera.transform.Find("AimLookAt");
        MultiAimConstraint[] BodyAimLayer =Player.transform.GetComponentsInChildren<MultiAimConstraint>();
        foreach(var animatedObject in BodyAimLayer)
        {
            animatedObject.data.sourceObjects = new WeightedTransformArray() { new WeightedTransform(cameraLookAt, 1) };
        }
        rigBuilder.Build();
        */
    }

}
