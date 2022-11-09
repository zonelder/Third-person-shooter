
using UnityEngine;

[System.Serializable]
public class Magazine 
{
    [SerializeField] private int _capacity;
    [SerializeField] private int _currentAmmoAmount;

    public void Reload()
    {
        _currentAmmoAmount = _capacity;
    }

    public bool TryShoot(int count)
    {
        if (count <= 0)
            throw new System.ArgumentOutOfRangeException();
        if (_currentAmmoAmount < count)
            return false;

         _currentAmmoAmount -= count;
        return true;
    }
}
