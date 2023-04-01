using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject playerGameObject;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.playerGameObject = GameObject.Find("Player");
        Instantiate(
            this.enemyPrefab,
            new Vector3(0, 0, 6),
            this.playerGameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
