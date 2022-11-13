using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private RaycastWeapon _weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
       var  ActiveWeapon = other.gameObject.GetComponent<WeaponSlots>();
        if(ActiveWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(_weaponPrefab);
            Debug.Log("enter");
            ActiveWeapon.Equip(newWeapon);
        }
    }
}
