using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    private GameObject _enemy;
    private float _enemySpeed;

    private void Start() {
        _enemySpeed = PlayerPrefs.GetFloat("speed", 3);    
    }
    private void Update() {
        if (_enemy == null) {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
            _enemy.GetComponent<WanderingAI>().updateSpeed(_enemySpeed);
        }
    }
    private void Awake() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    private void OnSpeedChanged(float value) {
        _enemySpeed = value; 
        if (_enemy != null) {
            _enemy.GetComponent<WanderingAI>().updateSpeed(_enemySpeed);
        }
    }
    private void OnDestroy() {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
}
