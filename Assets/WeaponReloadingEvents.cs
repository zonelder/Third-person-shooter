using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : UnityEvent<string>
{ }


public class WeaponReloadingEvents : MonoBehaviour
{
    private AnimationEvent _weaponReloadingEvent = new AnimationEvent(); 
    // Start is called before the first frame update
 
    public void OnAnimationEvent(string eventName)
    {
        _weaponReloadingEvent.Invoke(eventName);
    }
}
