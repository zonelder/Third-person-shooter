using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class WeaponRecoil : MonoBehaviour
{
    private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField] private CinemachineImpulseSource _impulsSource;

    private Animator _rigController;
    [SerializeField] private AnimationClip  _recoilAnimation;
    public const float s_unitMultipleY = 1000.0f;
    public const float s_unitMultipleX = 10.0f;
    [SerializeField] private Vector2[] _recoilPattern;

    [SerializeField] private float _duration;

    private float _verticalRecoil;
    private float _horizontalRecoil;
    private float _currentTime;
    private int _currentRecoilIndex;


   public void Reset()
    {
        _currentRecoilIndex = 0;
    }
    private int NextIndex(int index)
    {
        return (index + 1) % _recoilPattern.Length;
    }
    public void GenerateRecoil()
    {
        _currentTime = _duration;
        _impulsSource.GenerateImpulse(Camera.main.transform.forward);
        _horizontalRecoil = _recoilPattern[_currentRecoilIndex].x;
        _verticalRecoil = _recoilPattern[_currentRecoilIndex].y;
        _currentRecoilIndex = NextIndex(_currentRecoilIndex);

        _rigController.Play("Weapon_recoil",1,0.0f);

    }
    public void Construct(CinemachineFreeLook camera,Animator animator )
    {
        _rigController = animator;
        AnimatorOverrideController rigOverrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        _cinemachineFreeLook = camera;
        rigOverrideController["Weapon_recoil_empty"] = _recoilAnimation;
    }
     void Update()
    {
        if(_currentTime>0)
        {
            _cinemachineFreeLook.m_YAxis.Value -= (_verticalRecoil/s_unitMultipleY*Time.deltaTime)/_duration;
            _cinemachineFreeLook.m_XAxis.Value -= (_horizontalRecoil / s_unitMultipleX * Time.deltaTime) / _duration;
            _currentTime -= Time.deltaTime;
        }
     
    }
}
