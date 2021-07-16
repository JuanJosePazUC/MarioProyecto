using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockHit : MonoBehaviour
{
    private bool hitted = false;
    private bool hitted2 = false;
    private Animator animator;
    public GameObject mushroom;
    public GameObject flower;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){

            CombateMario mario = other.collider.GetComponent<CombateMario>();

            if(hitted == false){
                if(other.GetContact(0).normal.y >= 0.9){
                    if(mario.life == 1){
                        Instantiate(mushroom, transform.position, Quaternion.identity);
                    }else{ 
                        Instantiate(flower, transform.position, Quaternion.identity);
                    }
                    animator.SetBool("isHit",true);   
                    hitted = true;  
                    hitted2 = true;
                    Invoke("HittedFalse",0.1f);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            if(other.gameObject.name.Contains("Goomba")){
                GoombaCombat goombaCombat = other.collider.GetComponent<GoombaCombat>();
                if(other.GetContact(0).normal.y <= -0.9 && hitted2 == true){
                    goombaCombat.Dead("Hitted");
                }
            }
            
        }
    }

    public void HittedFalse (){
        hitted2 = false;
    }
}
