using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    public float speed = 3f;
    public bool canUse = true;
    public bool canGoDown = true;
    public bool playerFreeze = false;
    public bool canGoRight = false;
    public GameObject Block;
    private EndFlag endBlock;
    public Transform transformMario;



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(canUse){
                canUse = false;
                Rigidbody2D marioRb = other.gameObject.GetComponent<Rigidbody2D>();
                ControladorMario controladorMario = other.gameObject.GetComponent<ControladorMario>();
                Animator marioAnimator = other.gameObject.GetComponentInChildren<Animator>();
                Transform marioTransform = other.gameObject.GetComponent<Transform>();

                if(marioTransform.rotation.y == -1){ 
                    controladorMario.Girar();
                }

                marioAnimator.SetTrigger("Flag");
                marioRb.constraints = RigidbodyConstraints2D.FreezeAll;
                playerFreeze = true;
                controladorMario.enabled = false;
                StartCoroutine(Audio(marioRb, controladorMario));
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Transform marioTransform = other.gameObject.GetComponent<Transform>();
            if(canGoDown){
                marioTransform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }
    }

    IEnumerator Audio(Rigidbody2D marioRb, ControladorMario controladorMario){
        FindObjectOfType<AudioManager>().Play("FlagPole");
        FindObjectOfType<AudioManager>().Pause("MainTheme");
        yield return new WaitForSeconds(1.5f);
        canGoRight = true;
        marioRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        controladorMario.Girar();
        FindObjectOfType<AudioManager>().Play("StageClear");
    }

    private void FixedUpdate() {
        if(canGoRight){
            transformMario.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

}
