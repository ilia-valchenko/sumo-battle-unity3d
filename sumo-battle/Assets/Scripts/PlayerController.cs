using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float PlayerSpeed = 3.5f;

    private Rigidbody playerRigidBody;
    private GameObject focalPoint;

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
}
