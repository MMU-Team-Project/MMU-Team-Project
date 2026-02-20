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

    [SerializeField]
    private Vector3 spawnPoint1;

    [SerializeField]
    private Vector3 spawnPoint2;

    [SerializeField]
    private Vector3 spawnPoint3;

    [SerializeField]
    private List<Vector3> spawnPointList = new List<Vector3>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetTimeUntilSpawn();
        spawnPointList.Add(spawnPoint1);
        spawnPointList.Add(spawnPoint2);
        spawnPointList.Add(spawnPoint3);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <= 0)
        {
            //RandomisedArea();
            RandomSetLocations();

            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime); //spawn enemies a random time between variables
    }

    void RandomisedArea() //spawns randomly around the spawner
    {
        spawnLocation = new Vector3(Random.Range(transform.position.x-maxSpawnDist, transform.position.x+maxSpawnDist), 
        transform.position.y,
        Random.Range(transform.position.z-maxSpawnDist, transform.position.z+maxSpawnDist));

        Instantiate(enemy, spawnLocation, Quaternion.identity);
    }

    void RandomSetLocations() //randomly chooses from a list of spawn points
    {
        spawnLocation = spawnPointList[Random.Range(0, spawnPointList.Count)];

        Instantiate(enemy, spawnLocation, Quaternion.identity);
    }
}
