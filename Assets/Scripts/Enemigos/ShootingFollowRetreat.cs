using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHOOTINGFOLLOWRETREAT : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform Player;
    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;    
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, Player.position) > stoppingDistance){
            
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        
        } else if(Vector2.Distance(transform.position, Player.position) < stoppingDistance && Vector2.Distance(transform.position, Player.position) > retreatDistance){
           
            transform.position = this.transform.position;
        
        } else if(Vector2.Distance(transform.position, Player.position) < retreatDistance){

            transform.position = Vector2.MoveTowards(transform.position, Player.position, -speed * Time.deltaTime);
        
        }

        if(timeBtwShots <= 0){

            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
            
        }else{

            timeBtwShots -= Time.deltaTime;

        }
    }
}
