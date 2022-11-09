using UnityEngine;
public enum EnemyType
{
    Follower,
}
public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;

    public EnemyType Type => _enemyType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.4f);
    }
}
