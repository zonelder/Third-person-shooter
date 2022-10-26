using UnityEngine;
using Zenject;
using Cinemachine;


namespace Common.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] Transform _startPoint;
        [SerializeField] GameObject _playerPrefab;
        public override void InstallBindings()
        {
            BindHero();


        }
        private void BindHero()
        {
          /*  CharacterLocomotion Character = Container
                .InstantiatePrefabForComponent<CharacterLocomotion>(_playerPrefab, _startPoint.position, Quaternion.identity, null);

            Container
                .Bind<CharacterLocomotion>()
                .FromInstance(Character)
                .AsSingle();
          */
        }
    }

}


