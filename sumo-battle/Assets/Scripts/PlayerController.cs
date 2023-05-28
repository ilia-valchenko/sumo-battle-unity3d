using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    None,
    Default,
    Missile,
    Smash
}

public class PlayerController : MonoBehaviour
{
    private const float PlayerSpeed = 7f;
    private const float PowerUpForce = 15.0f;
    private const float CountdownLimitInSeconds = 7.0f;

    private PowerupType powerupType = PowerupType.None;
    private float floorY;
    private bool smashing = false;

    private Rigidbody playerRigidBody;
    private GameObject focalPoint;
    private GameObject tmpRocket;

    public GameObject powerupIndicator;
    public GameObject missilePrefab;

    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

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

        if (this.powerupType == PowerupType.Missile && Input.GetKeyDown(KeyCode.Space))
        {
            this.LaunchRocket();
        }

        if (this.powerupType == PowerupType.Smash && Input.GetKeyDown(KeyCode.Space))
        {
            this.smashing = true;
            StartCoroutine(Smash());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (this.powerupType != PowerupType.None)
        {
            return;
        }

        if (other.CompareTag("Powerup"))
        {
            this.powerupType = PowerupType.Default;
        }
        else if (other.CompareTag("SuperPowerup"))
        {
            this.powerupType = PowerupType.Missile;
        }
        else if (other.CompareTag("SmashPowerup"))
        {
            this.powerupType = PowerupType.Smash;
        }

        Destroy(other.gameObject);
        this.powerupIndicator.gameObject.SetActive(true);
        StartCoroutine(PowerupCountdownRoutine());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("SuperEnemy")) && this.powerupType == PowerupType.Default)
        {
            var enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = enemyRigidBody.position - this.playerRigidBody.position;
            enemyRigidBody.AddForce(awayFromPlayer * PowerUpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(CountdownLimitInSeconds);
        this.powerupType = PowerupType.None;
        this.powerupIndicator.gameObject.SetActive(false);
    }

    private void LaunchRocket()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            // Quaternion.identity
            // This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
            this.tmpRocket = Instantiate(this.missilePrefab, transform.position + Vector3.up, Quaternion.identity);
            // Fire(...) is my custom method from the RocketBehaviour script.
            this.tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    private IEnumerator Smash()
    {
        var enemies = this.FindEnemies();

        // Store the Y position before taking off.
        this.floorY = this.transform.position.y;

        // Calculate the amount of time we will go up.
        float jumpTime = Time.time + this.hangTime;

        while (Time.time < jumpTime)
        {
            // 'this.playerRigidBody.position' is where you are in the world.
            // Rigidbody.velocity is how fast the rigidbody is moving, IF you are using Physics (completely optional).

            // Move the player up while still keeping their X velocity.
            this.playerRigidBody.velocity = new Vector2(this.playerRigidBody.velocity.x, smashSpeed);
            yield return null;
        }

        // Now move the player down.
        while (this.transform.position.y > this.floorY)
        {
            this.playerRigidBody.velocity = new Vector2(this.playerRigidBody.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        if (enemies != null)
        {
            // Cycle through all enemies.
            foreach (var enemy in enemies)
            {
                // Apply an explosion force that originates from our position.
                if (enemy != null)
                {
                    enemy.GetComponent<Rigidbody>().AddExplosionForce(
                        explosionForce,
                        transform.position,
                        explosionRadius,
                        0.0f,
                        ForceMode.Impulse);
                }
            }
        }

        // We are no longer smashing, so set the boolean to false.
        this.smashing = false;
    }

    private IEnumerable<MonoBehaviour> FindEnemies()
    {
        var enemies = new List<MonoBehaviour>();
        enemies.AddRange(FindObjectsOfType<Enemy>());
        enemies.AddRange(FindObjectsOfType<SuperEnemy>());
        return enemies;
    }
}
