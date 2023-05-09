using UnityEngine;

public class SuperEnemy : MonoBehaviour
{
    private const float EnemySpeed = 6.0f;

    private Rigidbody enemyRigidBody;
    private GameObject playerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        this.enemyRigidBody = GetComponent<Rigidbody>();
        this.playerGameObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Vector1 - vector2 (minus)
        Vector3 lookDirection = (this.playerGameObject.transform.position - this.transform.position).normalized;
        this.enemyRigidBody.AddForce(lookDirection * EnemySpeed);
    }
}
