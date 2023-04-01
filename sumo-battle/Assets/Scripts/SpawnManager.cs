using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const float PositionLimit = 9f;

    private GameObject playerGameObject;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.playerGameObject = GameObject.Find("Player");
        Instantiate(this.enemyPrefab, this.GenerateRandomPosition(), this.playerGameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Vector3 GenerateRandomPosition()
    {
        var randomPositionX = Random.Range(-PositionLimit, PositionLimit);
        var randomPositionZ = Random.Range(-PositionLimit, PositionLimit);
        return new Vector3(randomPositionX, 0, randomPositionZ);
    }
}
