
using UnityEngine;
using Zenject;
public class PlayerPointer : MonoBehaviour
{
    private GameObject _player;
    [Inject]
    private void Construct(CharacterLocomotion character)
    {
        _player = character.gameObject;
        Debug.Log("PlayerSet");
        transform.position = character.transform.position;
    }
}
