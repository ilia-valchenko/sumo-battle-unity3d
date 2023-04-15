using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const float PositionLimit = 9f;

    private GameObject playerGameObject;
    private int numberOfAliveEnemies;
    private int numberOfEnemiesToSpawn = 1;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.playerGameObject = GameObject.Find("Player");
        this.SpawnEnemyWave(this.numberOfEnemiesToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        this.numberOfAliveEnemies = FindObjectsOfType<Enemy>() == null ? 0 : FindObjectsOfType<Enemy>().Length;

        if (this.numberOfAliveEnemies == 0)
        {
            this.SpawnEnemyWave(++this.numberOfEnemiesToSpawn);
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
}
