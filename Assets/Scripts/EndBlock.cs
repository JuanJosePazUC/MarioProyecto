using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlock : MonoBehaviour
{
    public GameObject stick;
    private EndFlag endFlag;

    private void Start() {
        endFlag = stick.GetComponent<EndFlag>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            Transform marioTransform = other.gameObject.GetComponent<Transform>();
            ControladorMario controladorMario = other.gameObject.GetComponent<ControladorMario>();
            if(endFlag.playerFreeze){
                controladorMario.Girar();
                marioTransform.Translate(Vector2.left * 1f);
                endFlag.canGoDown = false;
            }
        }
    }
}
