using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float speed = 1;
    public bool isSpawning = true;
    BoxCollider2D boxCollider2D;

    private void Start() {  
        FindObjectOfType<AudioManager>().Play("PowerUpAppears");
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    void FixedUpdate()
    {

        if(isSpawning){
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        
    }   

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Player")){
            CombateMario mario = other.collider.GetComponent<CombateMario>();
            FindObjectOfType<ScoreManager>().ScoreUp(1000,gameObject.transform);
            FindObjectOfType<AudioManager>().Play("PowerUp");
            if(mario.life >= 3){
                Destroy(gameObject);
            }else{
                mario.life = 3;
                mario.animator.SetTrigger("Change");
                Destroy(gameObject);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("ItemBlock")){
            boxCollider2D.isTrigger = false;
            isSpawning = false;
            speed = 0;
        }
    }




}

