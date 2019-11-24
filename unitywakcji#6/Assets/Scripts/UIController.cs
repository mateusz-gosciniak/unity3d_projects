using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel = null;
    [SerializeField] private Text timeLabel = null;
    [SerializeField] private Text endGameLabel = null;
    [SerializeField] private SettingsPopup settingsPopup = null;
    [SerializeField] private GameObject[] HealthImges = null; // powinno być pobierane od ustawień gracza, a tutaj tylko obrazek
    private int _score;

    private void Start() {
        _score = 0;
        scoreLabel.text = _score.ToString();
        settingsPopup.Close();
        endGameLabel.gameObject.SetActive(false);
    }
    private void Update() {
        timeLabel.text = Time.realtimeSinceStartup.ToString();
        if (Input.GetKey("escape")){
            Application.Quit();
        }
    }
    public void OnOpenSettings() {
        settingsPopup.Open();
    }
    public void OnPointerDown() {
        //Debug.Log("Mouse click");
    }
    void Awake() {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger<int>.AddListener(GameEvent.HEALTH_CHANGED, OnGettingDamage);
    }
    private void OnGettingDamage(int value){
        if (value < 0) return;

        if(HealthImges != null) {
            for (int i = 0; i < HealthImges.Length - value; i++) {
                if(HealthImges[HealthImges.Length - 1 - i] != null) {
                    HealthImges[HealthImges.Length - 1 - i].SetActive(false);
                }
            }

            if (value == 0){
                if (endGameLabel != null){
                    endGameLabel.gameObject.SetActive(true);
                }
                for (int i = 0; i < HealthImges.Length - value; i++){
                    Destroy(HealthImges[i].gameObject);
                }
            }
        }
    }
    private void OnEnemyHit(){
        _score += 1;
        scoreLabel.text = _score.ToString();
    }
    private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger<int>.AddListener(GameEvent.HEALTH_CHANGED, OnGettingDamage);
    }
}
