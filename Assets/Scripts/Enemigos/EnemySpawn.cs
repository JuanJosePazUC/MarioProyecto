using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    private Transform spawnPoint;
    public GameObject enemy;

    private void Start() {
        spawnPoint = GetComponent<Transform>();
    }

    public void SpawnEnemy(){
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        Destroy(gameObject);
    }

}
