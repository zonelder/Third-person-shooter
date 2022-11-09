using UnityEngine;

[CreateAssetMenu(menuName = "WeaponAnimationData")]
public class WeaponAnimationData:ScriptableObject
{
    [SerializeField] private AnimationClip _weaponEquip;
    [SerializeField] private AnimationClip _weaponReloading;
    [SerializeField] private AnimationClip _weaponRecoil;

    public AnimationClip Equip => _weaponEquip;
    public AnimationClip Reload => _weaponReloading;

    public AnimationClip Recoil => _weaponRecoil;
}