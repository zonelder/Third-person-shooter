using UnityEngine;

public class UnitRotation : MonoBehaviour
{
    private float _turnSpeed = 15f;
    private Camera _targetCamera;
    private void Start()
    {
        _targetCamera = Camera.main;
    }
    private void FixedUpdate()
    {
        RotateAccordingCamera();

    }


    public void RotateAccordingCamera()
    {
        float yawCamera = _targetCamera.transform.rotation.eulerAngles.y;
        //_rigid.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.fixedDeltaTime));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.fixedDeltaTime);
    }

}
