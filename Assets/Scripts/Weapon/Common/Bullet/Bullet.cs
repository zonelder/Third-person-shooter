using UnityEngine;
using System;
    public class Bullet
    {
        public Action<RaycastHit> OnHit;
        private float _lifeTime;
        private Vector3 _initialPosition;
        private Vector3 _initialVelocity;
        private BulletInfo _bulletInfo;
        //необходимо оптимизация 
        public TrailRenderer Tracer;

        public bool IsHit { get; private set; }

        public void HitSomething()
        {
    
            IsHit = true;
        }

        public bool IsOutlived => _lifeTime >= _bulletInfo.MaxLifeTime;
        public void UpdateState(float delta)
        {
            Vector3 p0 = GetBulletPosition();
            _lifeTime += delta;
            Vector3 p1 = GetBulletPosition();
            RaycastSegment(p0,p1);
        }
        private void RaycastSegment(Vector3 start, Vector3 end)
        {
            Vector3 deltaPosition = end - start;
            if (!IsHit && Physics.Raycast(start, deltaPosition, out RaycastHit hitInfo, deltaPosition.magnitude))
            {
                OnHit?.Invoke(hitInfo);
                Tracer.transform.position = hitInfo.point;
                HitSomething();

                var rigidBody = hitInfo.collider.GetComponent<Rigidbody>();
                if (rigidBody)
                {
                    rigidBody.AddForceAtPosition(deltaPosition.normalized * 20, hitInfo.point, ForceMode.Impulse);
                }

                var hitBox = hitInfo.collider.GetComponent<HitBox>();
                if (hitBox)
                {
                    hitBox.OnRayCastHit(_bulletInfo.Damage, hitInfo);
                }

            }
            else
            {
               Tracer.transform.position = end;
            }

        }
        public  Vector3 GetBulletPosition()
        {
            //p+v*t+0.5*g*t*t;
            Vector3 currentPosition = _initialPosition + _initialVelocity * _lifeTime + 0.5f * _bulletInfo.Drop*Vector3.down * (_lifeTime * _lifeTime);
            Tracer.transform.position = currentPosition;
            return currentPosition;

        }

        public static Bullet Create(Vector3 position,Vector3 velocity,BulletType type)
        {
            Bullet bullet = new Bullet();
            bullet._bulletInfo = type.Info;
            bullet._initialPosition = position;
            bullet._initialVelocity = velocity;
            bullet._lifeTime = 0.0f;
            bullet.Tracer = MonoBehaviour.Instantiate(type.Tracer, position, Quaternion.identity);
            bullet.Tracer.AddPosition(position);
            return bullet;
        }
        private Bullet() { }
    }
