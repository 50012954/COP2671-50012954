using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject obstacle2Prefab;
    public GameObject powerupPrefab;

    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private float obstacle2Delay = 5;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Start spawning obstacles at set intervals
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnObstacle2", startDelay + obstacle2Delay, repeatRate);

        // Start spawning powerups at random intervals
        StartPowerupSpawn();
    }

    void SpawnObstacle()
    {
        // Spawn obstacle if the game is active
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }

    void SpawnObstacle2()
    {
        // Spawn second type of obstacle if the game is active
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstacle2Prefab, spawnPos, obstacle2Prefab.transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        if (!playerControllerScript.gameOver)
        {
            Vector3 powerupSpawnPos = new Vector3(spawnPos.x, 2, spawnPos.z);
            Instantiate(powerupPrefab, powerupSpawnPos, powerupPrefab.transform.rotation);
        }

        StartPowerupSpawn(); // Schedule the next powerup spawn
    }

    void StartPowerupSpawn()
    {
        float randomInterval = Random.Range(5f, 10f);
        Invoke("SpawnPowerup", randomInterval);
    }
}