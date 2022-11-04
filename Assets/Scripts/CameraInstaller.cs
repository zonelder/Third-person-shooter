using UnityEngine;
using Zenject;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class CameraInstaller :MonoInstaller
{
    [SerializeField] private CinemachineFreeLook _playerCamera;
    public override void InstallBindings()
    {

        CinemachineFreeLook Cinemachine =Container.
            InstantiatePrefabForComponent<CinemachineFreeLook>(_playerCamera, Vector3.zero, Quaternion.identity,null);
        Container.
            Bind<CinemachineFreeLook>().
            FromInstance(Cinemachine).
            AsSingle().NonLazy();
    }
}
