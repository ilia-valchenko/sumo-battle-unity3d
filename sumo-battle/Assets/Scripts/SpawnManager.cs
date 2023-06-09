﻿using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const float PositionLimit = 9f;

    private GameObject playerGameObject;
    private int numberOfEnemiesToSpawn = 1;
    private int waveCount = 0;

    public GameObject enemyPrefab;
    public GameObject superEnemyPrefab;
    public GameObject powerupPrefab;
    public GameObject superPowerupPrefab;
    public GameObject smashPowerupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.playerGameObject = GameObject.Find("Player");
        this.SpawnEnemyWave(this.numberOfEnemiesToSpawn);
        this.SpawnPowerup();
        this.SpawnSuperPowerup();
        this.SpawnSmashPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetNumberOfAliveEnemies() == 0)
        {
            if (this.CanSpawnSuperEnemy())
            {
                this.SpawnSuperEnemy();
            }
            else
            {
                this.SpawnEnemyWave(++this.numberOfEnemiesToSpawn);
            }

            this.SpawnPowerup();
            this.SpawnSuperPowerup();
            this.SpawnSmashPowerup();
            this.waveCount++;
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        var randomPositionX = Random.Range(-PositionLimit, PositionLimit);
        var randomPositionZ = Random.Range(-PositionLimit, PositionLimit);
        return new Vector3(randomPositionX, 0, randomPositionZ);
    }

    private void SpawnEnemyWave(int numberOfEnemiesToSpawn)
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Instantiate(this.enemyPrefab, this.GenerateRandomPosition(), this.playerGameObject.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        Instantiate(this.powerupPrefab, this.GenerateRandomPosition(), this.powerupPrefab.gameObject.transform.rotation);
    }

    private void SpawnSuperPowerup()
    {
        Instantiate(this.superPowerupPrefab, this.GenerateRandomPosition(), this.superPowerupPrefab.gameObject.transform.rotation);
    }

    private void SpawnSmashPowerup()
    {
        Instantiate(this.smashPowerupPrefab, this.GenerateRandomPosition(), this.smashPowerupPrefab.gameObject.transform.rotation);
    }

    private bool CanSpawnSuperEnemy()
    {
        return this.waveCount != 0 && this.waveCount % 3 == 0;
    }

    private int GetNumberOfAliveEnemies()
    {
        var numberOfEnemies = FindObjectsOfType<Enemy>() == null ? 0 : FindObjectsOfType<Enemy>().Length;
        var numberOfSuperEnemies = FindObjectsOfType<SuperEnemy>() == null ? 0 : FindObjectsOfType<SuperEnemy>().Length;

        return numberOfEnemies + numberOfSuperEnemies;
    }

    private void SpawnSuperEnemy()
    {
        Instantiate(this.superEnemyPrefab, this.GenerateRandomPosition(), this.playerGameObject.transform.rotation);
    }
}
