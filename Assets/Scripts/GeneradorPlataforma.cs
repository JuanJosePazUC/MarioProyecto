using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPlataforma : MonoBehaviour
{
    [SerializeField] private GameObject plataforma;
    [SerializeField] private float tiempoSiguienteSpawn;
    [SerializeField] private float tiempoEntreSpawns;
    private void Update()
    {
        tiempoSiguienteSpawn -= Time.deltaTime;
        if (tiempoSiguienteSpawn <= 0)
        {
            Instantiate(plataforma, transform.position, Quaternion.identity);
            tiempoSiguienteSpawn = tiempoEntreSpawns;
        }
    }
}
