using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    //public GameObject projectile;

    public Transform Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, Player.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

        }
        else if(Vector3.Distance(transform.position, Player.position) > stoppingDistance && Vector3.Distance(transform.position, Player.position) < stoppingDistance)
        {
            transform.position = this.transform.position;
        }
        else if(Vector3.Distance(transform.position, Player.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, -speed * Time.deltaTime);
        }

        /*if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }*/
    }
}