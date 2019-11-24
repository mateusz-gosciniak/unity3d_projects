using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private int _health;

    public int Health { get => _health; }
    void Start() {
        _health = 5;
        Messenger<int>.Broadcast(GameEvent.HEALTH_CHANGED, _health);
    }
    public void Hurt(int damage) {
        _health -= damage;
        Messenger<int>.Broadcast(GameEvent.HEALTH_CHANGED, _health);
    }
}
