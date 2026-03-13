using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float Speed = 25f;
    [SerializeField] float Gravity = 9.81f;
    [SerializeField] float ArrowLifetime = 10;

    private Vector3 velocity;
    private bool stopped = false;

    void Start()
    {
        // Use arrow's forward direction as initial direction
        velocity = transform.forward * Speed;

        // Set arrow to destroy itself after ArrowLifetime
        Destroy(gameObject, ArrowLifetime);
    }

    void Update()
    {
        if (stopped) return;

        float dt = Time.deltaTime;

        velocity += Vector3.down * Gravity * dt;
        transform.position += velocity * dt;

        // Rotate arrow to face movement direction
        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(velocity.magnitude > 0) StopArrow(other.gameObject);

    }

    void OnCollisionEnter(Collision collision)
    {
        if(velocity.magnitude > 0) StopArrow(collision.gameObject);
    }

    void StopArrow(GameObject Parent)
    {
        stopped = true;
        velocity = Vector3.zero;
        transform.parent = Parent.transform; //Set parent so if hit object moves it'll follow :P
    }
}
