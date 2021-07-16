using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public float velocidadSalto = 3f;
    private void Start() {
        rb.velocity = transform.right * speed;
    }

    private void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {

        foreach (ContactPoint2D point in other.contacts){
            if(point.normal.y >= 0.9){
                rb.AddForce(Vector2.up * velocidadSalto, ForceMode2D.Impulse);
                
            }else{
                Destroy(gameObject);
            }
        }



        if(other.gameObject.CompareTag("Enemy")){
            if(other.gameObject.name.Contains("Goomba")){
                GoombaCombat goomba = other.collider.GetComponent<GoombaCombat>();
                goomba.Dead("Hitted");
                Destroy(gameObject);
            }
            if(other.gameObject.name.Contains("Koopa")){
                KoopaCombat koopa = other.collider.GetComponent<KoopaCombat>();
                koopa.Life--;
                Destroy(gameObject);
            }   

        }
    }

}
