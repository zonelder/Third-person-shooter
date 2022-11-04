using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RaycastWeapon _weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
       var  ActiveWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(ActiveWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(_weaponPrefab);
            Debug.Log("enter");
            ActiveWeapon.Equip(newWeapon);
        }
    }
}
