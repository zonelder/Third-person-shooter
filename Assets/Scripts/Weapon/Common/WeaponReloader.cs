using System.Collections;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField] private Animator _rigController;
    private bool _isReloading = false;

    public void TryReload(RaycastWeapon reloadingWeapon)
    {
        if (!_isReloading)
        {
            _rigController.SetTrigger("Reload");
            _isReloading = true;
            StartCoroutine(WaitTillEndReloadAnimation(reloadingWeapon));
        }
    }
    private IEnumerator WaitTillEndReloadAnimation(RaycastWeapon weapon)
    {
        do
        {
            yield return new WaitForEndOfFrame();

        } while (_rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

        weapon?.Magazine.Reload();
        // better say Reload(RaycastWeapon weapon){weapon?.Reload()} or always know that raycast weapon has a Magazine(it can be or not)
        _isReloading = false;
    }
}
