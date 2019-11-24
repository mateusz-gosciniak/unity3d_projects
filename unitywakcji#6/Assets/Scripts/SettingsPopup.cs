using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Slider speedSlider = null;
    [SerializeField] private InputField nameInputField = null;
    private void Start() {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 3);
        nameInputField.text = PlayerPrefs.GetString("name", "name");
    }
    public void Open(){
        gameObject.SetActive(true);
    }
    public void Close(){
        gameObject.SetActive(false);
    }
    public void OnSubmitName(string name){
        PlayerPrefs.SetString("name", name);
    }
    public void OnSpeedValue(float speed){
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
        PlayerPrefs.SetFloat("speed", speed);
    }
    public void OnReset(){
        SceneManager.LoadScene("SampleScene");
    }
}
