using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            FindObjectOfType<AudioManager>().Play("Coin");
            FindObjectOfType<ScoreManager>().ColectCoin(1);
            Destroy(gameObject);
        }
        
    }
}
