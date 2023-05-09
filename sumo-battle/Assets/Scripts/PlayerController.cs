using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float PlayerSpeed = 3.5f;
    private const float PowerUpForce = 15.0f;
    private const float CountdownLimitInSeconds = 7.0f;

    private Rigidbody playerRigidBody;
    private GameObject focalPoint;
    private GameObject tmpRocket;

    public bool hasPowerup = false;
    public bool hasSuperPowerup = false;
    public GameObject powerupIndicator;
    public GameObject missilePrefab;

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
        this.powerupIndicator.gameObject.transform.position = this.transform.position + new Vector3(0, -0.5f, 0);

        if (this.hasSuperPowerup && Input.GetKeyDown(KeyCode.Space))
        {
            this.LaunchRocket();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Powerup") || other.CompareTag("SuperPowerup")) && (!this.hasPowerup || !this.hasSuperPowerup))
        {
            this.hasPowerup = other.CompareTag("Powerup");
            this.hasSuperPowerup = other.CompareTag("SuperPowerup");
            Destroy(other.gameObject);
            this.powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("SuperEnemy")) && this.hasPowerup)
        {
            var enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = enemyRigidBody.position - this.playerRigidBody.position;
            enemyRigidBody.AddForce(awayFromPlayer * PowerUpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(CountdownLimitInSeconds);
        this.hasPowerup = false;
        this.hasSuperPowerup = false;
        this.powerupIndicator.gameObject.SetActive(false);
    }

    private void LaunchRocket()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            // Quaternion.identity
            // This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
            this.tmpRocket = Instantiate(this.missilePrefab, transform.position + Vector3.up, Quaternion.identity);
            // Fire(...) is my custom method.
            this.tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }
}
