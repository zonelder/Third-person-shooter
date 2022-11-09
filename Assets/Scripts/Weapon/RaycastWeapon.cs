using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[RequireComponent(typeof(WeaponRecoil))]
public class RaycastWeapon : MonoBehaviour
{
    public ActiveWeapon.WeaponSlot weaponSlot;
    public WeaponAnimationData AnimationData;
    private bool _isFiring;
    [Header("Weapon stats")]
    [SerializeField] private BulletType _currentBulletType;
    [Tooltip("bullets per second")]
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Magazine _magazine;

    [Header("Effects")]
    [SerializeField] private ParticleSystem[] _muzzleFlash;
    [SerializeField] private ParticleSystem _hitEffect;


    private float _accumulatedTime;
    private float FireInterval =>1.0f/_fireRate;



    [SerializeField] private Transform _raycastOrigin;

    [HideInInspector]public Transform RaycastDestinationTarget;
    private List<Bullet> _bullets = new List<Bullet>();

    public WeaponRecoil Recoil { get; set; }

    public Magazine Magazine => _magazine;

    public void EmitHitEffect(RaycastHit hitInfo)
    {
        _hitEffect.transform.position = hitInfo.point;
        _hitEffect.transform.forward = hitInfo.normal;
        _hitEffect.Emit(1);
    }

    // сейчас так получается что закликивание кнопки выстрела ускоряет стрельбу что не приемлимо. надо исправить
    public void StartFiring()
    {
        _isFiring = true;
        _accumulatedTime = 0.0f;
        Recoil.Reset();
    }
    public void UpdateFiring(float deltaTime)
    {
        _accumulatedTime += deltaTime;
        while(_accumulatedTime>=0.0f)
        {
            FireBullet();
            _accumulatedTime -= FireInterval;
        }
    }

    public void Update()
    {
        if(_isFiring)
        {
            UpdateFiring(Time.deltaTime);
        }
        if(_bullets.Count>0)
        {
            foreach(var bullet in _bullets)
            {
                bullet.UpdateState(Time.deltaTime);
            }
            DestroyExcessBullet();
        }
    }
    private void DestroyExcessBullet()
    {

        for(int i=0;i<_bullets.Count;i++)
        {
            if (_bullets[i].IsOutlived || _bullets[i].IsHit)
            {
                _bullets[i].OnHit -= EmitHitEffect;
                _bullets.RemoveAt(i);

            }
        }
    }
    private void FireBullet()
    {
      if(!_magazine.TryShoot(1))
      {
            return;
      }
          foreach (var flashPart in _muzzleFlash)
          flashPart.Emit(1);

        var bullet = Bullet.Create(_raycastOrigin.position, (RaycastDestinationTarget.position - _raycastOrigin.position).normalized * _bulletSpeed,_currentBulletType);

        bullet.OnHit +=EmitHitEffect;
        _bullets.Add(bullet);

        Recoil.GenerateRecoil();
    }

    public void StopFiring()
    {
        _isFiring = false;
    }
    private void Awake()
    {
        Recoil = GetComponent<WeaponRecoil>();
        RaycastDestinationTarget = Camera.main.transform.Find("CameraTarget");
    }

}
