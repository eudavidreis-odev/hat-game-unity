using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTrigger : MonoBehaviour
{
    private GameController gameController;
    private UIController uIController;

    private void Start() {
        gameController = FindObjectOfType<GameController>();
        uIController = FindObjectOfType<UIController>();
    }
    private void OnTriggerEnter2D(Collider2D otherObjct) {
        if(otherObjct.gameObject.CompareTag("Destroyer")){
            DestroyObject();
        }else if(otherObjct.gameObject.CompareTag("Point")){
            gameController.score++;
            uIController.IncreaseScore();
            DestroyObject();
        }
    }
    public void DestroyObject(){
        Destroy(this.gameObject);
    }

}
