using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

[SerializeField]
    private GameObject enemy; //to do: either position spawners where we want enemies to be or
                              //       set points to determine a random area to spawn enemies

    [SerializeField]
    private float minSpawnTime;

    [SerializeField]
    private float maxSpawnTime;

    [SerializeField]
    private float timeUntilSpawn;

    [SerializeField]
    private Vector3 spawnLocation;
    
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
            spawnLocation = new Vector3(Random.Range(transform.position.x-25, transform.position.x+25), 
            transform.position.y, 
            Random.Range(transform.position.z-25, transform.position.z+25));

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
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
