using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranaPlant : MonoBehaviour
{
    [SerializeField] private Transform puntoMaximo;
    [SerializeField] private Transform puntoMinimo;
    [SerializeField] private float velocidad;
    [SerializeField] private float tiempoDeEspera;
    private Vector3 target;
    private void Start()
    {
        target = puntoMaximo.position;
    }

    private void Update()
    {
        if (transform.position == puntoMaximo.position)
        {
            StartCoroutine(WaitChangeTarget(puntoMinimo.position));
        }
        if (transform.position == puntoMinimo.position)
        {
            StartCoroutine(WaitChangeTarget(puntoMaximo.position));
        }
        transform.position = Vector2.MoveTowards(transform.position, target, velocidad * Time.deltaTime);
    }

    private IEnumerator WaitChangeTarget(Vector3 position)
    {
        yield return new WaitForSeconds(tiempoDeEspera);
        target = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CombateMario mario = other.GetComponent<CombateMario>();
            ChangeMario marioChange = other.GetComponentInChildren<ChangeMario>();
            mario.life--;
            mario.animator.SetTrigger("ChangeDown");
        }
    }
}
