using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageBlinckEffect : MonoBehaviour
{
    private Health _health;
    [SerializeField] private float _blinckIntensity;
    private float _baseIntensity = 1.0f;
    [SerializeField] private float _blinckDuration;
    private float _blinckTimer;
    private SkinnedMeshRenderer _mesh;
    // Start is called before the first frame update


    private void Awake()
    {
        _mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        _health = GetComponent<Health>();
        
    }

    public void StartBlinck()
    {
        _blinckTimer = _blinckDuration;
    }
    void Update()
    {
        if(_blinckTimer>0)
        {
            _blinckTimer -= Time.deltaTime;

            float lerp = Mathf.Clamp01(_blinckTimer / _blinckDuration);
            float intensity = lerp * _blinckIntensity+_baseIntensity;
            _mesh.material.color = Color.white * intensity;
        }

    }

    private void OnEnable()
    {
        _health.OnGetDamage += StartBlinck;
    }
    private void OnDisable()
    {
        _health.OnGetDamage -= StartBlinck;
    }
}
