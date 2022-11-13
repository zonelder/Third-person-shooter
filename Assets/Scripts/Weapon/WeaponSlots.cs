using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IRotate))]
public class WeaponSlots : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }
    private IRotate _pointOfView;
    [SerializeField] private int _activeWeaponIndex;
    public Transform CrossHairTarget;
    private RaycastWeapon[] _equipedWeapons = new RaycastWeapon[2];
    [SerializeField] private Transform[] _weaponSlots;
    [SerializeField] private Animator _rigController;
    [Space(10)]
    [SerializeField] private bool _switchWeaponOnPickUp;
    private bool _isHostelWeapon;

    public bool IsHostelWeapon
    {
        get => _isHostelWeapon;
        set
        {
            _rigController.SetBool("HostelWeapon", value);
            _isHostelWeapon = value;
        }
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeaponAt(_activeWeaponIndex);
    }

    private void Awake()
    {
        _pointOfView = GetComponent<IRotate>();
    }
    private void Start()
    {
        _isHostelWeapon = _rigController.GetBool("HostelWeapon");
        var existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if(existingWeapon)
        {
            Equip(existingWeapon);
        }

    }
    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var currentWeaponInSlot = GetWeaponAt(weaponSlotIndex);
        UnequipCurrentAt(weaponSlotIndex);

        currentWeaponInSlot = newWeapon;
        currentWeaponInSlot.Construct(CrossHairTarget, _pointOfView, _rigController);
        currentWeaponInSlot.transform.SetParent(_weaponSlots[weaponSlotIndex],worldPositionStays:false);
        _equipedWeapons[weaponSlotIndex] = currentWeaponInSlot;

        if (_switchWeaponOnPickUp)
            SetActiveWeapon(currentWeaponInSlot.weaponSlot);

    }
    public void SetActiveWeapon(WeaponSlot slot)
    {
        int hostelIndex = _activeWeaponIndex;
        int activateIndex = (int)slot;
        if(hostelIndex ==activateIndex)
        {
            // by doing that we will skip HostelWeapon segmet of code
            hostelIndex = -1;
            // This thing need no be reworked;
        }
        StartCoroutine(SwitchWeapon(hostelIndex, activateIndex));
    }
    public void UnequipCurrentAt(int weaponSlotIndex)
    {
        if (_equipedWeapons[weaponSlotIndex])
        {
            Destroy(_equipedWeapons[weaponSlotIndex].gameObject);
        } 
    }
    private RaycastWeapon GetWeaponAt(int index)
    {
        if (index < 0 || index >= _equipedWeapons.Length)
        {
            return null;
        }
        return _equipedWeapons[index];
    }

    private IEnumerator HostelWeapon(int index)
    {
        const string animationString = "Hostel_weapon";
        var weapon = GetWeaponAt(index);
        if(weapon && !_isHostelWeapon)
        {

            IsHostelWeapon = true;
            yield return new WaitForEndOfFrame();
            do
            {
                yield return new WaitForEndOfFrame();
            } while (_rigController.GetCurrentAnimatorStateInfo(0).IsName(animationString)&& _rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }

    private IEnumerator ActivateWeapon(int index)
    {
        const string animationString = "Weapon_equip";
        var weapon = GetWeaponAt(index);
        if (weapon)
        {
            _activeWeaponIndex = index;
            //<all animator stuff>
            IsHostelWeapon = false;
            _rigController.SetBool("WeaponEquiped", true);

            _equipedWeapons[_activeWeaponIndex].ReplaceAnimationIn(_rigController);
            //</all animator stuff>

            do
            {
                yield return new WaitForEndOfFrame();
            } while (_rigController.GetCurrentAnimatorStateInfo(0).IsName(animationString)&& _rigController.GetCurrentAnimatorStateInfo(0).normalizedTime<1.0f);// живет ровно один кадр
        }
    }

    private IEnumerator SwitchWeapon(int HostelIndex,int activateIndex)
    {
        yield return StartCoroutine(HostelWeapon(HostelIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));

    }
}
