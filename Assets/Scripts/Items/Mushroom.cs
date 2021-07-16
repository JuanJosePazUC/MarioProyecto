using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float speed = 1;
    public bool isSpawning = true;
    Rigidbody2D Rigidbody2D;
    BoxCollider2D boxCollider2D;

    private void Start() {
        FindObjectOfType<AudioManager>().Play("PowerUpAppears");
        Rigidbody2D = GetComponent<Rigidbody2D>();    
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        
        if(isSpawning){
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }else{
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Player")){
            CombateMario mario = other.collider.GetComponent<CombateMario>();
            FindObjectOfType<ScoreManager>().ScoreUp(1000,gameObject.transform);
            if(mario.life >= 2){
                Destroy(gameObject);
            }else{
                mario.life = 2;
                FindObjectOfType<AudioManager>().Play("PowerUp");
                mario.animator.SetTrigger("Change");
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){ 
            foreach(ContactPoint2D point in other.contacts){
                if(point.normal.x >= 0.9f){
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }else if(point.normal.x <= -0.9f){
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("ItemBlock")){
            boxCollider2D.isTrigger = false;
            isSpawning = false;
            Rigidbody2D.gravityScale = 2;
            speed = 2;
        }
    }
}

