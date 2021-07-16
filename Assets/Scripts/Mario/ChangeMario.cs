using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMario : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private Animator animator;
    private CombateMario combateMario;

    private void Start() {
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        combateMario = GetComponent<CombateMario>();
    }

    private void Update() {

        if(combateMario.life >= 2){
            circleCollider.enabled = true;
        }else{
            circleCollider.enabled = false;
        }

    }

    /*
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Item")){
            if(combateMario.life == 1){
                combateMario.life++;
                StartCoroutine(Change());
            }else{
                //Incrementar Puntos
            }
            
            Destroy(other.gameObject);

        }
    }
    */

}
