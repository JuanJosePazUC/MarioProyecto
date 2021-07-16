using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopLimit : MonoBehaviour
{
    public GameObject stick;
    public float waitTime = 6f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            stick.GetComponent<EndFlag>().canGoRight = false;
            StartCoroutine(Level2());
        }
    }

    IEnumerator Level2(){
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
