using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaCombat : MonoBehaviour
{
    public int Life = 2;
    public Animator animator;
    public PatrolPlatform patrol;
    private BoxCollider2D BoxCollider2D;
    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D Rigidbody2D;
    private MovimientoKoopa movimientoKoopa;
    public bool shellMoving = false;
    public GameObject kooopaFireHitted;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        movimientoKoopa = GetComponent<MovimientoKoopa>();
    }

    private void Update()
    {
        animator.SetInteger("Life", Life);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ControladorMario player = other.collider.GetComponent<ControladorMario>();
            CombateMario mario = other.collider.GetComponent<CombateMario>();
            ChangeMario marioChange = other.collider.GetComponentInChildren<ChangeMario>();

            if (other.GetContact(0).normal.y <= -0.9)
            {
                player.Bounce();
                if (Life == 2)
                {
                    Life--;
                    BoxCollider2D.enabled = false;
                    movimientoKoopa.speed = 0;
                }
                else
                {
                    if (shellMoving)
                    {
                        movimientoKoopa.speed = 0;
                        shellMoving = false;
                    }
                    else
                    {
                        movimientoKoopa.speed = 8;
                        shellMoving = true;
                    }
                }
            }
            else
            {
                if (Life == 2 || shellMoving == true)
                {
                    mario.life--;
                    mario.animator.SetTrigger("ChangeDown");
                }
                else if (shellMoving == false)
                {
                    if (other.GetContact(0).normal.x >= 0.9)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                    }
                    else if (other.GetContact(0).normal.x <= -0.9)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    shellMoving = true;
                    movimientoKoopa.speed = 8;
                }
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.name.Contains("Goomba"))
            {
                if (shellMoving)
                {
                    GoombaCombat goomba = other.collider.GetComponent<GoombaCombat>();
                    goomba.Life = 0;
                    goomba.Dead("Hitted");
                }
            }
            if (other.gameObject.name.Contains("Koopa"))
            {
                if (shellMoving)
                {
                    KoopaCombat koopa = other.collider.GetComponent<KoopaCombat>();
                    Destroy(koopa.gameObject);
                }
            }

        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<ScoreManager>().ScoreUp(100, gameObject.transform);
            FindObjectOfType<AudioManager>().Play("Kick");
            Instantiate(kooopaFireHitted, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
