using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WeaponRecoil))]
public class RaycastWeapon : MonoBehaviour
{
    public ActiveWeapon.WeaponSlot weaponSlot;
    [SerializeField] private FireType _fireType;

    [SerializeField]private WeaponAnimationData AnimationData;// change from animation data to cpecific override animation controller
    private bool _isFiring;
    private bool _shouldFire;
    [Header("Weapon stats")]
    [SerializeField] private BulletType _currentBulletType;
    [Tooltip("bullets per second")]
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Magazine _magazine;

    [Header("Effects")]
    [SerializeField] private ParticleSystem[] _muzzleFlash;
    [SerializeField] private ParticleSystem _hitEffect;

    [SerializeField] private Transform _raycastOrigin;
    private Transform _raycastDestinationTarget;

    private WeaponRecoil _recoil;

    private float _accumulatedTime;
    private float FireInterval =>1.0f/_fireRate;


    private List<Bullet> _bullets = new List<Bullet>();


    public Magazine Magazine => _magazine;


    public void Construct(Transform destinationTarget, IRotate Rotator, Animator animator)
    {
        _raycastDestinationTarget = destinationTarget;
        _recoil.Construct(Rotator, animator);
    }


    // сейчас так получается что закликивание кнопки выстрела ускоряет стрельбу что не приемлимо. надо исправить
    public void StartFiring()
    {
        if(!_isFiring)
        {
            _isFiring = true;
            _accumulatedTime = 0.0f;
            _recoil.Reset();
            _shouldFire = true;
        }
    }
    public void StopFiring()
    {
        if (_fireType.CanStopManually)
            _shouldFire = false;
    }
    public void ReplaceAnimationIn(AnimatorOverrideController overrideController)
    {
        overrideController["Equip_animation"] = AnimationData.Equip;
        overrideController["Weapon_reload_empty"] = AnimationData.Reload;
        overrideController["Weapon_recoil_empty"] = AnimationData.Recoil;
    }
    private void UpdateFiring(float deltaTime)
    {
        _accumulatedTime += deltaTime;
        while (_accumulatedTime >= 0.0f)
        {
            if (_shouldFire)
            {
                FireBullet();
                _accumulatedTime -= FireInterval;
                _shouldFire = _fireType.OnBulletShoot();

            }
            else
            {
                _isFiring = false;
                break;
            }

        }
    }

    private void Update()
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
    private void EmitHitEffect(RaycastHit hitInfo)
    {
        _hitEffect.transform.position = hitInfo.point;
        _hitEffect.transform.forward = hitInfo.normal;
        _hitEffect.Emit(1);
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

        var bullet = Bullet.Create(_raycastOrigin.position, (_raycastDestinationTarget.position - _raycastOrigin.position).normalized * _bulletSpeed,_currentBulletType);

        bullet.OnHit +=EmitHitEffect;
        _bullets.Add(bullet);
        //OnShoot?.Invoke();

        _recoil.GenerateRecoil();
    }
    private void Awake()
    {
        _recoil = GetComponent<WeaponRecoil>();
    }

}
