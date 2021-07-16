using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject entrada;
    public GameObject salida;
    public GameObject camara1;
    public GameObject camara2;
    public float waitTime = 0.7f;
    public bool flag = true;
    
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Rigidbody2D marioRb = other.gameObject.GetComponent<Rigidbody2D>();
            Transform marioTransform = other.gameObject.GetComponent<Transform>();
            ControladorMario marioControlador = other.gameObject.GetComponent<ControladorMario>();
            Animator marioAnimator = other.gameObject.GetComponentInChildren<Animator>();
            
            if(marioControlador.crouch && flag){
                FindObjectOfType<AudioManager>().Play("Pipe");
                flag = false;
                StartCoroutine(ChangePosition(marioTransform, marioAnimator, marioRb));
            }

        }        
    }

    IEnumerator ChangePosition (Transform marioTransform, Animator marioAnimator, Rigidbody2D marioRb){
        marioAnimator.SetTrigger("GoDown");
        marioRb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(waitTime);
        camara1.SetActive(false);
        camara2.SetActive(true);
        marioRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        marioTransform.position = salida.transform.position;
        FindObjectOfType<AudioManager>().Pause("MainTheme");
        FindObjectOfType<AudioManager>().Play("UnderWorldTheme");
    }

}
