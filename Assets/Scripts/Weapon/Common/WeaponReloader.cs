using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField] private Animator _rigController;
    [SerializeField] private ActiveWeapon _weaponHolder;
    [SerializeField] private KeyCode _reloadKey;
    private bool _isReloading = false;


    private void Update()
    {
        if(Input.GetKeyDown(_reloadKey))
        {
            if(!_isReloading)
            {
                _rigController.SetTrigger("Reload");
                _isReloading = true;
                StartCoroutine(WaitTillEndReloadAnimation());
            }
 
        }
    }

    private IEnumerator WaitTillEndReloadAnimation()
    {
        do
        {
            yield return new WaitForEndOfFrame();

        } while (_rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

        _weaponHolder.GetActiveWeapon()?.Magazine.Reload();
        _isReloading = false;
    }
}
