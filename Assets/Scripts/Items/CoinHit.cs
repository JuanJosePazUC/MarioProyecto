using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHit : MonoBehaviour
{
    private Animator animator;
    public GameObject coin;
    public int quantCoins;
    public bool hitted = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        animator.SetInteger("quantCoins",quantCoins);
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){

            CombateMario mario = other.collider.GetComponent<CombateMario>();

            if(other.GetContact(0).normal.y >= 0.9){
                if(quantCoins > 0){
                    FindObjectOfType<ScoreManager>().ColectCoin(1);
                    Instantiate(coin, transform.position, Quaternion.identity);
                    animator.SetTrigger("Hitted"); 
                    quantCoins--;
                    hitted = true;
                    Invoke("HittedFalse",0.1f);
                }      
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
