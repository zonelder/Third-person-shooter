using UnityEngine;
using Zenject;

public class EnemyFactory :IEnemyFactory
{
    private readonly DiContainer _diContainer;

    private const string c_followerPath = "Prefab/Enemy/FollowerEnemy";

    private Object _followerEnemyPrefab;

    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    public void Load()
    {
        _followerEnemyPrefab = Resources.Load(c_followerPath);

    }

    public void Create(EnemyType type,Vector3 at)
    {
        switch (type)
        {
            case EnemyType.Follower:
                _diContainer.InstantiatePrefab(_followerEnemyPrefab, at, Quaternion.identity, null);
                break;
        }

    }
}
