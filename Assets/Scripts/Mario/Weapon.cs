using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    private CombateMario combateMario;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    private void Start() {
        combateMario = GetComponent<CombateMario>();
    }
    private void Update() {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            if(combateMario.life >= 3){
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot(){
        FindObjectOfType<AudioManager>().Play("FireBall");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
