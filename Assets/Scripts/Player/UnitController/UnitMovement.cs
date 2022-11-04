using UnityEngine;


public class UnitMovement:MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] CharacterController _conltroller;
    [SerializeField] Vector3 _velocity;
    private const float _baseSpeed = 3.0f;
    [SerializeField] private float _speedAmplifier;
    private Vector2 _input;

    private void Update()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
        if (_animator != null)
        {
            _animator.SetFloat("InputX", _input.x);
            _animator.SetFloat("InputY", _input.y);
        }


        _velocity = VectorAccordingInput() * _baseSpeed * _speedAmplifier;
        _conltroller.Move(_velocity*Time.deltaTime);

    }

    private Vector3 VectorAccordingInput()
    {
        float maxInput = (Mathf.Abs(_input.x) > Mathf.Abs(_input.y)) ? Mathf.Abs(_input.x) : Mathf.Abs(_input.y);
        _input.Normalize();
        Vector3 vectorAccortingInput = Vector3.zero;
        vectorAccortingInput.x = _input.x;
        vectorAccortingInput.z = _input.y;

        vectorAccortingInput = _conltroller.transform.TransformDirection(vectorAccortingInput * maxInput);

        return vectorAccortingInput;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _velocity);
    }
}
