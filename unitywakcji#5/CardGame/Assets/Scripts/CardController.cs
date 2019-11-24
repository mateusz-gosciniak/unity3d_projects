using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private GameObject cardBack = null;
    [SerializeField] private SceneController controller = null;
    private int _id;

    public int id{
        get {return _id;}
    }
    public void OnMouseDown() {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }    
    }
    public void SetCard(int id, Sprite image){
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void Unreveal(){
        cardBack.SetActive(true);
    }
}
