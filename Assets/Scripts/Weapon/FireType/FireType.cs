using UnityEngine;

public enum FireTypeName
{
    Single=0,
    Auto=1,
    Burst=2
}
[System.Serializable]
public class FireType
{
    [SerializeField] private FireTypeName _fireType;
    [Tooltip("effect work only if firetype is Burst")][SerializeField]private int _burstCount = 3;
    private int _countReleased;
    public bool CanStopManually => _fireType == FireTypeName.Auto;


    public bool OnBulletShoot()
    {
        switch (_fireType)
        {
            case FireTypeName.Single:
                return false;
            case FireTypeName.Burst:
                {
                    _countReleased += 1;
                    if (_countReleased >= _burstCount)
                    {
                        _countReleased = 0;
                        return false;
                    }
                }
                break;

        }
        return true;
    }
}
