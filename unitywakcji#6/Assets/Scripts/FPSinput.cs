using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSinput : MonoBehaviour
{
    private CharacterController _charController = null;
    public float speed = 8.0f;
    public float gravity = -9.8f;
    public const float baseSpeed = 6.0f;

    void Start() {
        _charController = GetComponent<CharacterController>();
    }
    void Update() {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        float deltaY = Input.GetAxis("Jump") * -2 * gravity;

        Vector3 movement = new Vector3(deltaX, 0.0f, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity + deltaY;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
    void Awake() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    private void OnSpeedChanged(float value) {
        speed = baseSpeed * value;
    }
    private void OnDestroy() {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
}
