using UnityEngine;

public interface IEnemyFactory
{
    void Create(EnemyType type, Vector3 at);
    void Load();
}