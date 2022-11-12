using UnityEngine;
public class WeaponRecoil : MonoBehaviour
{
    private IRotate _pointOfView;

    private Animator _rigController;
    public const float s_unitMultiple = 10.0f;
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
        _horizontalRecoil = _recoilPattern[_currentRecoilIndex].x;
        _verticalRecoil = _recoilPattern[_currentRecoilIndex].y;
        _currentRecoilIndex = NextIndex(_currentRecoilIndex);

        _rigController.Play("Weapon_recoil",1,0.0f);

    }
    public void Construct(IRotate Rotator,Animator animator )
    {
        _rigController = animator;
        _pointOfView = Rotator;
    }
     private void Update()
    {
        if(_currentTime>0)
        {
            _pointOfView.Rotate(_horizontalRecoil / s_unitMultiple * Time.deltaTime / _duration ,-(_verticalRecoil / s_unitMultiple * Time.deltaTime) / _duration);
            _currentTime -= Time.deltaTime;
        }
     
    }
}
