using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "bulletType",fileName ="bulletType/default",order =1)]
public partial class BulletType : ScriptableObject
{

    [SerializeField] private BulletInfo _info;

    [SerializeField] private TrailRenderer _BulletTracerEffect;

    public TrailRenderer Tracer => _BulletTracerEffect;

    public BulletInfo Info => _info;

}
