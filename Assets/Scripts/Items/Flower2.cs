using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower2 : MonoBehaviour
{
    public float speed = 1;
    public bool isSpawning = true;
    BoxCollider2D boxCollider2D;

    private void Start() {  
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    void FixedUpdate()
    {
        /*
        if(isSpawning){
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }else{

        }
        */
        
    }   

    /*
    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Player")){
            CombateMario mario = other.collider.GetComponent<CombateMario>();
            if(mario.life >= 3){
                //Puntos
                Destroy(gameObject);
            }else{
                mario.life = 3;
                StartCoroutine(ChangeCoroutine(mario));
            }
            
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("ItemBlock")){
            Debug.Log("Contacto perdido");
            boxCollider2D.isTrigger = false;
            isSpawning = false;
            speed = 0;
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
    }


    IEnumerator ChangeCoroutine(CombateMario mario)
    {
        
        mario.animator.SetBool("isChanging",true);
        
        yield return new WaitForSeconds(0.01f);

        mario.animator.SetBool("isChanging",false);
        Destroy(gameObject);
    }


}
