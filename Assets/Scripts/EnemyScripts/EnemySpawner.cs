using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

[SerializeField]
    private GameObject enemy; 

    [SerializeField]
    private float minSpawnTime;

    [SerializeField]
    private float maxSpawnTime;

    [SerializeField]
    private float timeUntilSpawn;

    [SerializeField]
    private Vector3 spawnLocation;

    [SerializeField]
    private int maxSpawnDist;  //maximum spawn distance from spawner
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <= 0)
        {
            spawnLocation = new Vector3(Random.Range(transform.position.x-maxSpawnDist, transform.position.x+maxSpawnDist), 
            transform.position.y, //this sets the spawnpoint to a random spot in the area around the spawner
            Random.Range(transform.position.z-maxSpawnDist, transform.position.z+maxSpawnDist));

            Instantiate(enemy, spawnLocation, Quaternion.identity);
            SetTimeUntilSpawn();
        }

        //if (Input.GetKeyDown("f"))
        //{
        //    Instantiate(enemy, transform.position, Quaternion.identity);
        //}
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime); //spawn enemies a random time between variables
    }
}
