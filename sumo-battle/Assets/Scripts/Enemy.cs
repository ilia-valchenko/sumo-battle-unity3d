using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float EnemySpeed = 3.0f;
    private const float BottomLimit = -15.0f;

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
        // Vector - vector (minus)
        Vector3 lookDirection = (this.playerGameObject.transform.position - this.transform.position).normalized;
        this.enemyRigidBody.AddForce(lookDirection * EnemySpeed);

        if (this.transform.position.y < BottomLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
