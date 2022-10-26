using UnityEngine;

public class HitBox : MonoBehaviour
{
    [HideInInspector] public Health  Health{get;set;}
 
    public void OnRayCastHit(float damage,RaycastHit Hit)
    {

        Health.GetDamage(damage);
    }
}
