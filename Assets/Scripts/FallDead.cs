using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player")){
            CombateMario mario = other.GetComponent<CombateMario>();
            mario.life = 0;
        }

        if(other.gameObject.CompareTag("Enemy")){
            Destroy(other.gameObject,0.5f);
        }

    } 
}
