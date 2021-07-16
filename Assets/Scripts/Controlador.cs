using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    [Header("Movimiento Horizontal")]
    public float velocidadMoviemiento = 5f;
    public Vector2 direccion;
    private bool mirandoDerecha = false;

    [Header("Movimiento Vertical")]
    public float velocidadSalto = 7f;
    public float jumpDelay = 0.3f;
    private float jumpTimer;

    [Header("Componentes")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    [Header("Fisicas")]
    public float maxSpeed = 5f;
    public float linearDrag = 5f;
    public float gravedad = 1f;
    public float multiplicadorCaida = 3f;


    [Header("Colision")]
    public bool isGrounded;
    public float groundCheck = 0.3f;
    public Vector3 colliderOffset;

    [Header("Animaciones")]
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundCheck, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundCheck, groundLayer);
        
        animator.SetFloat("Velocidad", Mathf.Abs(rb.velocity.x));

        if(isGrounded){
            animator.SetBool("isJumping", false);
        }

        if(Input.GetButtonDown("Jump")){
            jumpTimer = Time.time + jumpDelay;
        }

        direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        

    }

    void FixedUpdate() {
        MoverPersonaje(direccion.x);
        ModificarFisicas();
        if(jumpTimer > Time.time && isGrounded){
            animator.SetBool("isJumping", true);
            Salto();
        }
    }

    void MoverPersonaje(float horizontal){
        rb.AddForce(Vector2.right * horizontal * velocidadMoviemiento);

        if((horizontal > 0 && !mirandoDerecha) || (horizontal < 0 && mirandoDerecha)){
            Girar();
        }

        if(Mathf.Abs(rb.velocity.x) > maxSpeed){
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void ModificarFisicas(){
        bool cambiandoDireccion = (direccion.x > 0 && rb.velocity.x < 0) || (direccion.x < 0 && rb.velocity.x > 0);
        
        if(isGrounded){
            if(Mathf.Abs(direccion.x) < 0.4f || cambiandoDireccion){
                rb.drag = linearDrag;
            }else{
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }else{
            rb.gravityScale = gravedad;
            rb.drag = linearDrag * 0.15f;
            if(rb.velocity.y < 0){
                rb.gravityScale = gravedad * multiplicadorCaida;
            }else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){
                rb.gravityScale = gravedad * (multiplicadorCaida / 2);
            }
        }
    }
    void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.rotation = Quaternion.Euler(0, mirandoDerecha ? 0 : 180, 0);
    }

    void Salto(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * velocidadSalto, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundCheck);   
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundCheck);    
    }
}

