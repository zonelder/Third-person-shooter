using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [SerializeField] Rigidbody _rigid;
    [SerializeField] Vector3 _velocity;
    private const float _baseSpeed = 3.0f;
    [SerializeField] private float _speedAmplifier;
    public Rigidbody Rigidbody => _rigid;

    private void FixedUpdate()
    {
        _rigid.AddForce(_velocity-_rigid.velocity,ForceMode.Impulse);
    }

    public void SetTargetDirection(Vector3 target)
    {
        _velocity = target * _baseSpeed * _speedAmplifier;
    }

    private void Awake()
    {
        _rigid.constraints = RigidbodyConstraints.FreezeRotation;
        _rigid.isKinematic = false;
        gameObject.layer = LayerMask.NameToLayer("unitController");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _velocity);
    }
}
