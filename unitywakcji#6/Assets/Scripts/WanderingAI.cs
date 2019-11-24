using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab = null;
    private GameObject _fireball = null;
    private bool _alive = true;
    public float speed = 3.0f;
    private const float baseSpeed = 3.0f;
    public float obstacleRange = 5.0f;

    public bool IsAlive { get => _alive; }

    void Start() {
        _alive = true;
    }
    void Update() {
        if (_alive) {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>()) {
                    if (_fireball == null) {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                } else if(hit.distance < obstacleRange) {
                    float angle = UnityEngine.Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }
    public void SetAlive(bool alive){
        _alive = alive;
    }
    private void Awake() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    private void OnSpeedChanged(float value){
        updateSpeed(value);
    }
    private void OnDestroy() {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    public void updateSpeed(float value){
        speed = baseSpeed * value;
    }
}
