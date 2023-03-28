using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private GameObject focalPoint;

    public float playerSpeed = 1f;

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
        this.playerRigidBody.AddForce(this.focalPoint.transform.forward * this.playerSpeed * verticalInput);
    }
}
