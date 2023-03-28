using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 120f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //// Alternative
        //if (Input.GetKey(KeyCode.D))
        //{
        //    this.transform.Rotate(Vector3.up, 1);
        //    //this.transform.RotateAround()
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    this.transform.Rotate(Vector3.up, -1);
        //}



        // Requires to use arrows on your keyboard
        // "Horizontal" is case-sensitive
        var horizantalInput = Input.GetAxis("Horizontal");
        this.transform.Rotate(Vector3.up, horizantalInput * this.rotationSpeed * Time.deltaTime);
    }
}
