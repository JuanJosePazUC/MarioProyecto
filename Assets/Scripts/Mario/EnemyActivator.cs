using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("EnemySpawn")){
            EnemySpawn spawn = other.gameObject.GetComponent<EnemySpawn>();
            spawn.SpawnEnemy();
        }
    }

}
