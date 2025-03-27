using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed = 10f;  // Adjust speed as needed

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
