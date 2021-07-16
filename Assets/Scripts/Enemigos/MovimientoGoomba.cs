using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoGoomba : MonoBehaviour
{

    public float speed = 2;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        if(Mathf.Abs(rb.velocity.x) > 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground")){
            foreach(ContactPoint2D point in other.contacts){
                if(point.normal.x >= 0.9f){
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }else if(point.normal.x <= -0.9f){
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }

    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            foreach(ContactPoint2D point in other.contacts){
                if(point.normal.x >= 0.9f){
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }else if(point.normal.x <= -0.9f){
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
