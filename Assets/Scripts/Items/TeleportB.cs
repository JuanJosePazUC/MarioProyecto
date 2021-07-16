using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportB : MonoBehaviour
{
    public GameObject entrada;
    public GameObject salida;
    public GameObject camara1;
    public GameObject camara2;
    public float waitTime = 0.7f;

    private void OnTriggerEnter2D(Collider2D other) {
         if(other.CompareTag("Player")){
            Transform marioTransform = other.gameObject.GetComponent<Transform>();
            ControladorMario marioControlador = other.gameObject.GetComponent<ControladorMario>();
            Animator marioAnimator = other.gameObject.GetComponentInChildren<Animator>();
            Rigidbody2D marioRb = other.gameObject.GetComponent<Rigidbody2D>();
            FindObjectOfType<AudioManager>().Play("Pipe");
            StartCoroutine(ChangePosition(marioTransform, marioAnimator,marioRb));
        }          
    }   

    IEnumerator ChangePosition (Transform marioTransform, Animator marioAnimator, Rigidbody2D marioRb){
        marioRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        marioTransform.position = salida.transform.position;
        camara1.SetActive(false);
        camara2.SetActive(true);
        marioAnimator.SetTrigger("GoUp");
        yield return new WaitForSeconds(waitTime);
        marioRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        FindObjectOfType<AudioManager>().Pause("MainTheme");
        FindObjectOfType<AudioManager>().Play("MainTheme");
        FindObjectOfType<AudioManager>().Pause("UnderWorldTheme");
    }


}
