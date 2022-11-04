using UnityEngine;


public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Rigidbody[] _rigids;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigids = GetComponentsInChildren<Rigidbody>();
        DeactivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        foreach (var rigidBody in _rigids)
        {
            rigidBody.isKinematic = false;
            //rigidBody.WakeUp();
        }
        _animator.enabled = false;
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidBody in _rigids)
        {
            rigidBody.isKinematic = true;
            //rigidBody.Sleep();
        }
        _animator.enabled = true;
    }
}
