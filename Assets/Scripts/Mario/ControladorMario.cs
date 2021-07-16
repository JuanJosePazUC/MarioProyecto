using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMario : MonoBehaviour
{
    [Header("Movimiento Horizontal")]
    public float velocidadMoviemiento = 100f;
    public Vector2 direccion;
    private bool mirandoDerecha = true;

    [Header("Movimiento Vertical")]
    public float velocidadSalto = 8f;
    public float jumpDelay = 0.3f;
    private float jumpTimer;

    [Header("Componentes")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    private CircleCollider2D circleCollider2D;
    private CombateMario combateMario;

    [Header("Fisicas")]
    public float maxSpeed = 4f;
    public float linearDrag = 5f;
    public float gravedad = 1f;
    public float multiplicadorCaida = 3f;


    [Header("Colision")]
    public bool isGrounded;
    public float groundCheck = 0.6f;
    public Vector3 colliderOffset;
    public bool crouch = false;
    public float ceilingCheck = 0.6f;
    public bool canStand;

    [Header("Animaciones")]
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        combateMario = GetComponent<CombateMario>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundCheck, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundCheck, groundLayer);
        canStand = Physics2D.Raycast(transform.position + colliderOffset, Vector2.up, ceilingCheck, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.up, ceilingCheck, groundLayer);

        if(Input.GetButtonDown("Jump")){
            jumpTimer = Time.time + jumpDelay;
        }

        direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetButton("Crouch") && isGrounded){
            crouch = true;
        }else if(!Input.GetButton("Crouch") && !canStand){
            crouch = false;
        }

    }

    void FixedUpdate() {

        if(combateMario.life == 1 || (combateMario.life > 1 && !crouch)){
            MoverPersonaje(direccion.x);
        }
        

        ModificarFisicas();

        if(isGrounded){
            animator.SetBool("isGrounded", true);
            if(Mathf.Abs(rb.velocity.x) > 0.3){
                animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            }else{
                animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            }
        }else{
            animator.SetBool("isGrounded",false);
        }

        animator.SetBool("isCrouching",crouch); 


        if(jumpTimer > Time.time && isGrounded && (combateMario.life == 1 || (combateMario.life > 1 && !crouch))){
            Salto();
        }

        if(combateMario.life > 1){
            if(crouch){
                circleCollider2D.enabled = false;
            }else{
                circleCollider2D.enabled = true;
            }
        }

        if(crouch == true && canStand == true && combateMario.life > 1){
            rb.AddForce(Vector2.right * direccion.x * 0.2f);
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

    public void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.rotation = Quaternion.Euler(0, mirandoDerecha ? 0 : 180, 0);
    }

    void Salto(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * velocidadSalto, ForceMode2D.Impulse);
        jumpTimer = 0;
        FindObjectOfType<AudioManager>().Play("SuperJump");
    }
    public void Bounce(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * (velocidadSalto/2), ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundCheck);   
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundCheck);    
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.up * ceilingCheck);   
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.up * ceilingCheck);   
    }

}
