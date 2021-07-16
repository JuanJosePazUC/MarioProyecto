using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaCombat : MonoBehaviour
{
    public float Life = 1f;
    public Animator animator;
    private BoxCollider2D BoxCollider2D;
    private Rigidbody2D Rigidbody2D;
    private MovimientoGoomba movimientoGoomba;

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        movimientoGoomba = GetComponent<MovimientoGoomba>();
    }
    
    private void Update() {
        if(Life <= 0){
            Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            movimientoGoomba.enabled = false;
            BoxCollider2D.enabled = false;     
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            ControladorMario player = other.collider.GetComponent<ControladorMario>();
            CombateMario mario = other.collider.GetComponent<CombateMario>();
            ChangeMario marioChange = other.collider.GetComponentInChildren<ChangeMario>();
            ScoreManager score = other.collider.GetComponent<ScoreManager>();

            if(other.GetContact(0).normal.y <= -0.9){
                player.Bounce();
                Dead("isDead");
            }else {    
                mario.life--;
                mario.animator.SetTrigger("ChangeDown");
            }
        }
        
    }

    public void Dead(string Tipo){
        Life--;
        FindObjectOfType<ScoreManager>().ScoreUp(100,gameObject.transform);
        if(Tipo == "isDead"){
            FindObjectOfType<AudioManager>().Play("Stomp");
            animator.SetTrigger("isDead");
            Destroy(gameObject, 0.5f);
        }

        if(Tipo == "Hitted"){
            FindObjectOfType<AudioManager>().Play("Kick");
            animator.SetTrigger("Hitted");
            Destroy(gameObject, 0.8f);
        }
    }

}

