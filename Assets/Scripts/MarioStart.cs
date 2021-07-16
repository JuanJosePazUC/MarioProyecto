using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioStart : MonoBehaviour
{
    public float waitTime = 3f;
    public GameObject timer;
    public GameObject AudioManager;
    public GameObject mario;
    public Rigidbody2D marioRb;

    private void Start() {
        StartCoroutine(MarioScreen());
        AudioManager = GameObject.Find("AudioManager");
        mario = GameObject.FindGameObjectWithTag("Player");
        marioRb = mario.GetComponent<Rigidbody2D>();
    }

    IEnumerator MarioScreen(){
        yield return new WaitForSeconds(waitTime);
        timer.SetActive(true);
        AudioManager.SetActive(true);
        gameObject.SetActive(false);
        marioRb.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }
}
