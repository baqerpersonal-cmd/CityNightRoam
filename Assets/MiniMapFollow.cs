using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    private Transform car;

    void Start()
    {
        // Auto-finds car with Rigidbody (no drag needed)
        Rigidbody rb = FindObjectOfType<Rigidbody>();
        if (rb != null)
            car = rb.transform;
        else
            Debug.LogError("No car Rigidbody found!");
    }

    void LateUpdate()
    {
        if (car == null) return;

        // Follow car but keep camera height fixed
        transform.position = new Vector3(car.position.x, transform.position.y, car.position.z);
    }
}
