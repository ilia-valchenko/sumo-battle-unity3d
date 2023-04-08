using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float PlayerSpeed = 3.5f;
    private const float PowerUpForce = 15.0f;

    private Rigidbody playerRigidBody;
    private GameObject focalPoint;

    public bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        this.playerRigidBody = GetComponent<Rigidbody>();
        this.focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");
        // What about Time.deltaTime???
        //this.playerRigidBody.AddForce(Vector3.forward * this.playerSpeed * verticalInput);
        this.playerRigidBody.AddForce(this.focalPoint.transform.forward * PlayerSpeed * verticalInput);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            this.hasPowerup = true;
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && this.hasPowerup)
        {
            var enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = enemyRigidBody.position - this.playerRigidBody.position;
            enemyRigidBody.AddForce(awayFromPlayer * PowerUpForce, ForceMode.Impulse);
        }
    }
}
