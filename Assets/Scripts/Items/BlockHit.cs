using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    private Animator animator;
    public bool hitted = false;

    private void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Player")){
       
            CombateMario mario = other.collider.GetComponent<CombateMario>();
            animator.SetInteger("marioLife",mario.life);

            if(other.GetContact(0).normal.y >= 0.9){
                animator.SetTrigger("Hitted");     
                hitted = true;
                if(mario.life >= 2){
                    FindObjectOfType<AudioManager>().Play("BreakBlock");
                    Destroy(gameObject,0.3f);
                }
                FindObjectOfType<AudioManager>().Play("Bump");
                Invoke("HittedFalse",0.1f);
            }

        }   
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            if(other.gameObject.name.Contains("Goomba")){
                GoombaCombat goombaCombat = other.collider.GetComponent<GoombaCombat>();
                if(other.GetContact(0).normal.y <= -0.9 && hitted == true){
                    goombaCombat.Dead("Hitted");
                }
            }
            
        }
    }

    public void HittedFalse (){
        hitted = false;
    }

}
