using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPlatform : MonoBehaviour
{

    public float speed = 12;
    private bool movingRight = true;
    public Transform groundCheck;
    public float distance;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        if(Mathf.Abs(rb.velocity.x) > 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        if(groundInfo.collider == false){   
            if(movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }else{
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("Enemy")){
            foreach(ContactPoint2D point in other.contacts){
                if(point.normal.x >= 0.9f){
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }else if(point.normal.x <= -0.9f){
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
            }
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distance);   
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distance);    
    }
}
