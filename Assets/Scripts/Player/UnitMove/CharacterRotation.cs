using UnityEngine;

public class CharacterRotation : MonoBehaviour,IRotate
{
    private float _turnSpeed=15f;
    [SerializeField] private Rigidbody _rigid;
    [Header("Camera Settings")]
    [SerializeField] private Transform _cameraLookAt;
    [SerializeField] private Cinemachine.AxisState _xAxis;
    [SerializeField] private Cinemachine.AxisState _yAxis;
    [Space(5)]
    [SerializeField]private Animator _animator;

    [SerializeField] private KeyCode _aimKey;
    private void Update()
    {
        bool isAim = Input.GetKey(_aimKey);
        _animator.SetBool("isAim", isAim);
    }
    private void FixedUpdate()
    {
        _xAxis.Update(Time.fixedDeltaTime);
        _yAxis.Update(Time.fixedDeltaTime);

        _cameraLookAt.eulerAngles = new Vector3(_yAxis.Value, _xAxis.Value, 0);
        RotateAccordingTarget();
 
    }

    public void Rotate(float xInputValue,float yInputValue)
    {
        _xAxis.Value += xInputValue;
        _yAxis.Value += yInputValue;
    }

    public void RotateAccordingTarget()
    {
        _rigid.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,_xAxis.Value, 0), _turnSpeed * Time.fixedDeltaTime));
        _cameraLookAt.localEulerAngles= new Vector3(_yAxis.Value, 0, 0);
    }

}
