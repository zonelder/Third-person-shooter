using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    private float _turnSpeed=15f;
    private Camera _targetCamera;
    [SerializeField] private Rigidbody _rigid;
    [Header("Camera Settings")]
    [SerializeField] private Transform _cameraLookAt;
    [SerializeField] private Cinemachine.AxisState _xAxis;
    [SerializeField] private Cinemachine.AxisState _yAxis;

    [SerializeField]private Animator _animator;

    [SerializeField] private KeyCode _aimKey;
    private void Start()
    {
        _targetCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        bool isAim = Input.GetKey(_aimKey);
        if (isAim)
            Debug.Log("zoom");
        _animator.SetBool("isAim", isAim);
    }
    private void FixedUpdate()
    {
        _xAxis.Update(Time.fixedDeltaTime);
        _yAxis.Update(Time.fixedDeltaTime);

        _cameraLookAt.eulerAngles = new Vector3(_yAxis.Value, _xAxis.Value, 0);
        RotateAccordingCamera();
 
    }

    public void RotateCamera(float xInputValue,float yInputValue)
    {
        _xAxis.Value += xInputValue;
        _yAxis.Value += yInputValue;
    }

    public void RotateAccordingCamera()
    {
        _rigid.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,_xAxis.Value, 0), _turnSpeed * Time.fixedDeltaTime));
        _cameraLookAt.localEulerAngles= new Vector3(_yAxis.Value, 0, 0);
    }

}
