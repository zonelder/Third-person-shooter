using UnityEngine;

[System.Serializable]
    public class BulletInfo
    {
        [SerializeField] private float _maxLifeTime;
        [SerializeField] private float _damage;
        [SerializeField] private float _drop;
        public float MaxLifeTime => _maxLifeTime;
        public float Damage => _damage;

        public float Drop => _drop;
    }
