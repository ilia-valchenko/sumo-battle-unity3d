using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private const float LimitY = -15.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < LimitY)
        {
            Destroy(this.gameObject);
        }
    }
}
