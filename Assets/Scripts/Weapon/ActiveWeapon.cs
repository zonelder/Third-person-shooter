using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Zenject;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private bool _switchWeaponOnPickUp;
    public enum WeaponSlot 
    {
        Primary=0,
        Secondary=1
    }
    [SerializeField] private CharacterRotation  _characterAiming;//using for x y mouse input
    [SerializeField] private int _activeWeaponIndex;
    public Transform CrossHairTarget;
    private RaycastWeapon []_equipedWeapons = new RaycastWeapon[2];
    [SerializeField] private Transform[] _weaponSlots;
    [Space(10)]
    [Header("WeaponGrip")]

    [SerializeField] private Transform _weaponLeftGrip;
    [SerializeField] private Transform _weaponRightGrip;
    [Space(10)]
    [Header("weapon animation")]
    [SerializeField] private UnityEngine.Animations.Rigging.Rig _handIK;
    [SerializeField] private Animator _rigController;
    private AnimatorOverrideController _rigOverrideController;
    [Space(10)]

    [SerializeField] private KeyCode _fireKey;
    [SerializeField] private KeyCode _hostelWeaponKey;
    [SerializeField] private KeyCode _primaryWeaponKey;
    [SerializeField] private KeyCode _SecondaryWeaponKey;

    private bool _isHostelWeapon;


    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeaponAt(_activeWeaponIndex);
    }
    void Start()
    {
        _rigOverrideController = _rigController.runtimeAnimatorController as AnimatorOverrideController;
        _isHostelWeapon = _rigController.GetBool("HostelWeapon");
        var existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if(existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeaponAt(_activeWeaponIndex);
        if(weapon)
        { 
            if(!_isHostelWeapon)
            {
                if (Input.GetKeyDown(_fireKey))
                {
                    weapon.StartFiring();
                }
                if (Input.GetKeyUp(_fireKey))
                {
                    weapon.StopFiring();
                }
            }

            if(Input.GetKeyDown(_hostelWeaponKey))
            {
                _rigController.SetBool("HostelWeapon", !_isHostelWeapon);
                _isHostelWeapon = !_isHostelWeapon;
            }
        }
        if(Input.GetKeyDown(_primaryWeaponKey))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if(Input.GetKeyDown(_SecondaryWeaponKey))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var currentWeaponInSlot = GetWeaponAt(weaponSlotIndex);
        UnequipCurrentAt(weaponSlotIndex);

        currentWeaponInSlot = newWeapon;
        currentWeaponInSlot.RaycastDestinationTarget = CrossHairTarget;
        currentWeaponInSlot.Recoil.Construct( _characterAiming,_rigController);
        currentWeaponInSlot.transform.SetParent(_weaponSlots[weaponSlotIndex],worldPositionStays:false);
        _equipedWeapons[weaponSlotIndex] = currentWeaponInSlot;


        if (_switchWeaponOnPickUp)
            SetActiveWeapon(currentWeaponInSlot.weaponSlot);



      
    }
    private void SetActiveWeapon(WeaponSlot slot)
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
            Debug.Log("try to accses invalid weapon");
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
            _rigController.SetBool("HostelWeapon", true);
            _isHostelWeapon = true;
            yield return new WaitForEndOfFrame();
            do
            {
                yield return new WaitForEndOfFrame();
              /*  Debug.Log(_rigController.GetCurrentAnimatorClipInfo(0)[0].clip.name + " in   "
                        + ((_rigController.GetCurrentAnimatorStateInfo(0).speed == 1) ? ("equip") : ("hostel"))
                        + " " + _rigController.GetCurrentAnimatorStateInfo(0).normalizedTime);

                */

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
            _rigController.SetBool("HostelWeapon", false);
            _isHostelWeapon = false;
            _rigController.SetBool("WeaponEquiped", true);

            _rigOverrideController["Equip_animation"] = _equipedWeapons[_activeWeaponIndex].AnimationData.Equip;
            _rigOverrideController["Weapon_reload_empty"] = _equipedWeapons[_activeWeaponIndex].AnimationData.Reload;
            _rigOverrideController["Weapon_recoil_empty"] = _equipedWeapons[_activeWeaponIndex].AnimationData.Recoil;

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
