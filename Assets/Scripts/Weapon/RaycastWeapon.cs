using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class RaycastWeapon : MonoBehaviour
{
    public ActiveWeapon.WeaponSlot weaponSlot;
    // Start is called before the first frame update
    private bool _isFiring;
    [Header("Weapon stats")]
    [Tooltip("bullets per second")]
    [SerializeField] private float _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    //Better stay here than in bullet class' or use flyWeight patternt to adapt
    [SerializeField] private float _bulletDrop;
    [SerializeField] private float _magazineCapacity;
    [SerializeField] private float _currentAmmoCount;
   

    private float _accumulatedTime;
    private float FireInterval=>1.0f/_fireRate;
    [Header("Effects")]
    [SerializeField] private ParticleSystem[] _muzzleFlash;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private TrailRenderer _BulletTracerEffect;
    [SerializeField] private AnimationClip _weaponAnimation;
    [SerializeField] private AnimationClip _weaponReloading;
    public AnimationClip WeaponAnimation => _weaponAnimation;
    public AnimationClip WeaponReload => _weaponReloading;

    [SerializeField] private Transform _raycastOrigin;

    [HideInInspector]public Transform RaycastDestinationTarget;
    private float _maxBulletLifeTime = 3.0f;
    private RaycastHit _hitInfo;
    private List<Bullet> _bullets = new List<Bullet>();
    private Vector3 BulletGravityFactor=>Vector3.down*_bulletDrop;

    public WeaponRecoil Recoil { get; set; }
    [SerializeField] private GameObject _magazine;


    // Значение вычисляеться динамическ ходя достаточно было бы вычилисть только при изменение _bulletDrop(настройка импектора в помощь)


    public void ReloadMagazine()
    {
        _currentAmmoCount = _magazineCapacity;
    }
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
                Vector3 p0 = bullet.GetBulletPosition(BulletGravityFactor);
                bullet.UpdateTime(Time.deltaTime);
                Vector3 p1 = bullet.GetBulletPosition(BulletGravityFactor);
                RaycastSegment(p0, p1, bullet);
            }
            DestroyExcessBullet();
        }
    }

    private void RaycastSegment(Vector3 start,Vector3 end,Bullet bullet)
    {
        Vector3 deltaPosition = end - start;
        if (!bullet.IsHit&&Physics.Raycast(start,deltaPosition,out _hitInfo,deltaPosition.magnitude))
        {
            _hitEffect.transform.position = _hitInfo.point;
            _hitEffect.transform.forward = _hitInfo.normal;
            _hitEffect.Emit(1);
            bullet.Tracer.transform.position = _hitInfo.point;
            bullet.HitSomething();

            var rigidBody = _hitInfo.collider.GetComponent<Rigidbody>();
            if(rigidBody)
            {
                rigidBody.AddForceAtPosition(deltaPosition.normalized * 20, _hitInfo.point, ForceMode.Impulse);
                Debug.Log(rigidBody.gameObject.name);
            }

            var hitBox = _hitInfo.collider.GetComponent<HitBox>();
            if(hitBox)
            {
                hitBox.OnRayCastHit(_damage, _hitInfo);
            }
           
        }
        else
        {
            bullet.Tracer.transform.position = end;
        }
       
    }

    private void DestroyExcessBullet()
    {
        _bullets.RemoveAll(bullet => bullet.LifeTime > _maxBulletLifeTime || bullet.IsHit);
    }
    private void FireBullet()
    {
        if(_currentAmmoCount<=0)
        {
            return;
        }
        _currentAmmoCount--;
        foreach (var flashPart in _muzzleFlash)
            flashPart.Emit(1);

        var bullet = Bullet.Create(_raycastOrigin.position, (RaycastDestinationTarget.position - _raycastOrigin.position).normalized * _bulletSpeed,_BulletTracerEffect);
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
        //FireInterval = 1.0f / _fireRate;
        // _bulletGravityFactor = _bulletDrop * Vector3.down;
    }

    public class Bullet
    {
        private float _lifeTime;
        private Vector3 _initialPosition;
        private Vector3 _initialVelocity;
        //необходимо оптимизация 
        public TrailRenderer Tracer;

        public bool IsHit { get; private set; }

        public void HitSomething() => IsHit = true;

        public float LifeTime => _lifeTime;
        public void UpdateTime(float delta)
        {
            _lifeTime += delta;
        }
        public  Vector3 GetBulletPosition(Vector3 bulletGravityFactor)
        {
            //p+v*t+0.5*g*t*t;
            Vector3 currentPosition = _initialPosition + _initialVelocity * _lifeTime + 0.5f * bulletGravityFactor * (_lifeTime * _lifeTime);
            Tracer.transform.position = currentPosition;
            return currentPosition;

        }

        public static Bullet Create(Vector3 position,Vector3 velocity,TrailRenderer tracerEffect)
        {
            Bullet bullet = new Bullet();
            bullet._initialPosition = position;
            bullet._initialVelocity = velocity;
            bullet._lifeTime = 0.0f;
            bullet.Tracer = Instantiate(tracerEffect, position, Quaternion.identity);
            bullet.Tracer.AddPosition(position);
            return bullet;
        }
        private Bullet() { }
    }

}
