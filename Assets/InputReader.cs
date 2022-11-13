using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private WeaponSlots _slots;
    [SerializeField] private WeaponReloader _reloader;
    [SerializeField] private CharacterLocomotion _characterLocomotion;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Animator _bodyAnimator;
    [Space(10)]
    [Header("slots")]
    [SerializeField] private KeyCode _fireKey;
    [SerializeField] private KeyCode _hostelWeaponKey;
    [SerializeField] private KeyCode _primaryWeaponKey;
    [SerializeField] private KeyCode _SecondaryWeaponKey;
    [Space(10)]
    [Header("WeaponSpecial")]
    [SerializeField] private KeyCode _aimKey;
    [SerializeField] private KeyCode _reloadKey;

    private Vector2 _moveInput;

    private void Start()
    {
        if(!_slots || !_reloader ||!_cameraAnimator)
        {
            throw new System.NullReferenceException();
        }
    }
    private void Update()
    {
        var weapon = _slots.GetActiveWeapon();
        if (weapon)
        {
            CheckFireInput(weapon);
            CheckReloadInput(weapon);
            CheckHostelWeaponInput();
        }
        CheckPrimaryWeaponInput();
        CheckSecondaryWeaponInput();

        CheckAimInput();

        CheckMoveInput();

    }

    private void CheckMoveInput()
    {
        if (_bodyAnimator != null)
        {
            _moveInput.x = Input.GetAxis("Horizontal");
            _moveInput.y = Input.GetAxis("Vertical");
            _bodyAnimator.SetFloat("InputX", _moveInput.x);
            _bodyAnimator.SetFloat("InputY", _moveInput.y);
            _characterLocomotion.SetTargetDirection(VectorAccordingInput());

        }
    }

    private void CheckReloadInput(RaycastWeapon weapon)
    {
        if (Input.GetKeyDown(_reloadKey))
        {
            _reloader.TryReload(weapon);
        }
    }

    private void CheckAimInput()
    {
        bool isAim = Input.GetKey(_aimKey);
        _cameraAnimator?.SetBool("isAim", isAim);
    }

    private void CheckSecondaryWeaponInput()
    {
        if (Input.GetKeyDown(_SecondaryWeaponKey))
        {
            _slots.SetActiveWeapon(WeaponSlots.WeaponSlot.Secondary);
        }
    }

    private void CheckPrimaryWeaponInput()
    {
        if (Input.GetKeyDown(_primaryWeaponKey))
        {
            _slots.SetActiveWeapon(WeaponSlots.WeaponSlot.Primary);
        }
    }

    private void CheckHostelWeaponInput()
    {
        if (Input.GetKeyDown(_hostelWeaponKey))
        {
            _slots.IsHostelWeapon = !_slots.IsHostelWeapon;
        }
    }

    private void CheckFireInput(RaycastWeapon weapon)
    {
        if (!_slots.IsHostelWeapon)
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
    }
    private Vector3 VectorAccordingInput()
    {
        float maxInput = (Mathf.Abs(_moveInput.x) > Mathf.Abs(_moveInput.y)) ? Mathf.Abs(_moveInput.x) : Mathf.Abs(_moveInput.y);
        _moveInput.Normalize();
        Vector3 vectorAccortingInput = Vector3.zero;
        vectorAccortingInput.x = _moveInput.x;
        vectorAccortingInput.z = _moveInput.y;

        vectorAccortingInput = _characterLocomotion.Rigidbody.transform.TransformDirection(vectorAccortingInput * maxInput);

        return vectorAccortingInput;
    }
}
