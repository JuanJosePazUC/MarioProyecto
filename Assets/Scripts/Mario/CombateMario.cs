using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CombateMario : MonoBehaviour
{
    public int life = 1;
    public Animator animator;
    private bool lifeflag = true;
    private ControladorMario controlador;
    private BoxCollider2D BoxCollider2D;
    private Rigidbody2D Rigidbody2D;
    public GameObject scoreManager;
    public ScoreManager score;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controlador = GetComponent<ControladorMario>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.Find("ScoreManager");
        score = scoreManager.GetComponent<ScoreManager>();
    }
    void Update()
    {
        animator.SetFloat("Life",life);
        if(life <= 0){
            controlador.enabled = false;
            BoxCollider2D.enabled = false;
            animator.SetBool("isDead", true);
            Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            if(lifeflag){
                FindObjectOfType<ScoreManager>().LifeUp(-1);
                FindObjectOfType<AudioManager>().Mute("MainTheme");
                FindObjectOfType<AudioManager>().Mute("HurryMainTheme");
                FindObjectOfType<AudioManager>().Mute("UnderWorldTheme");
                FindObjectOfType<AudioManager>().Play("MarioDeath");
                lifeflag = false;
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Death(){
        if(score.vidas < 0){
            yield return new WaitForSeconds(3f);
            FindObjectOfType<AudioManager>().Play("GameOver");
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("Menu");
        }else{
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

}
