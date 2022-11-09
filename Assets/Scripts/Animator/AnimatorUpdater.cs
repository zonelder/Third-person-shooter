using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorUpdater : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _animator.enabled = true;
    }
}
