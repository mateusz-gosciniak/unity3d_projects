using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private CharacterController _player = null;
    public float speed = 10.0f;
    public float gravity = -9.8f;

    void Start(){
        _player = this.gameObject.GetComponent<CharacterController>();
    }
    void Update() {
        if (_player == null) return;

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaY = gravity + Input.GetAxis("Vertical") * speed;
        Vector3 translateVector = new Vector3(deltaX, deltaY, 0.0f);
        translateVector = Vector3.ClampMagnitude(translateVector, speed);
        translateVector *= Time.deltaTime;
        translateVector = transform.TransformDirection(translateVector);
        _player.Move(translateVector);
        
        if (Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
