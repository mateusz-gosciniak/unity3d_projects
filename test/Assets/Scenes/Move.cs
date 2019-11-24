using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController _playerControl = null;
    public float speed = 3.0f;
    public float gravity = -9.8f;

    private void Start() {
        _playerControl = this.gameObject.GetComponent<CharacterController>();
    }
    void Update()
    {    
        if (_playerControl == null) return;

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0.0f, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        _playerControl.Move(movement);
    }
}
