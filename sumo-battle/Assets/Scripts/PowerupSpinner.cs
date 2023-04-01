using UnityEngine;

public class PowerupSpinner : MonoBehaviour
{
    private const float RotationSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * RotationSpeed);
    }
}
