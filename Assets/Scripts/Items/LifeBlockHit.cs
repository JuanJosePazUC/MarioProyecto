using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBlockHit : MonoBehaviour
{
    private bool hitted = false;
    private Animator animator;
    public GameObject mushroomLife;
    private BoxCollider2D boxCollider2D;
    private PlatformEffector2D PlatformEffector2D;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        PlatformEffector2D = GetComponent<PlatformEffector2D>();
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){

            if(hitted == false){
                if(other.GetContact(0).normal.y >= 0.9){
                    gameObject.layer = LayerMask.NameToLayer("Ground");
                    boxCollider2D.usedByEffector = false;
                    PlatformEffector2D.enabled = false;
                    Instantiate(mushroomLife, transform.position, Quaternion.identity);
                    animator.SetBool("isHit",true);   
                    hitted = true;  
                    boxCollider2D.isTrigger = false;
                }
            }
        }
    }
}
