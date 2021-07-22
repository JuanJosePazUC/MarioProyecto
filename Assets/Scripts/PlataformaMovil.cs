using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float tiempoDeVida;
    private void Start() {
        Destroy(gameObject, tiempoDeVida);
    }
    private void Update() {
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);
    }
}
