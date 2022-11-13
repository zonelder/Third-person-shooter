using UnityEngine;
using Zenject;
using Cinemachine;
using UnityEngine.Animations.Rigging;


namespace Common.Infrastructure
{
    public class LocationInstaller : MonoInstaller,IInitializable
    {
        [SerializeField] Transform _startPoint;
        [SerializeField] GameObject _playerPrefab;
        [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;
        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindHero();
            BindEnemyFactory();

        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<LocationInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IEnemyFactory>().
                To<EnemyFactory>().
                AsSingle();
        }

        private void BindHero()
        {
            CharacterLocomotion Character = Container
                .InstantiatePrefabForComponent<CharacterLocomotion>(_playerPrefab, _startPoint.position, Quaternion.identity, null);

            Container
                .Bind<CharacterLocomotion>()
                .FromInstance(Character)
                .AsSingle();

            Camera mainCamera = Camera.main;
            RigBuilder rigBuilder = Character.transform.Find("BodyLayer").GetComponent<RigBuilder>();
            Transform cameraLookAt = mainCamera.transform.Find("AimLookAt");
            MultiAimConstraint[] BodyAimLayer = Character.transform.GetComponentsInChildren<MultiAimConstraint>();
            foreach (var animatedObject in BodyAimLayer)
            {
                animatedObject.data.sourceObjects = new WeightedTransformArray() { new WeightedTransform(cameraLookAt, 1) };
            }

 
            rigBuilder.Build();
            CrosshairTarget target = mainCamera.GetComponentInChildren<CrosshairTarget>();
            Character.gameObject.GetComponent<WeaponSlots>().CrossHairTarget = target.transform;

        }

        public void Initialize()
        {
            var enemyFactory = Container.Resolve<IEnemyFactory>();
            enemyFactory.Load();
            foreach(var marker in _enemySpawnPoints)
            {
                enemyFactory.Create(marker.Type, marker.transform.position);
            }
        }
    }

}


