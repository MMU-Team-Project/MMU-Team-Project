using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5;
    public float rotationSpeed = 10;
    public float currentRotation = 0;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0) //corrects diagonal speed
        {
            rb.linearVelocity = transform.forward * vertical * playerSpeed / Mathf.Sqrt(2) + transform.right * horizontal * playerSpeed / Mathf.Sqrt(2);
        }
        else
        {
            rb.linearVelocity = transform.forward * vertical * playerSpeed + transform.right * horizontal * playerSpeed;
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            currentRotation=currentRotation - rotationSpeed;
            transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            currentRotation = currentRotation + rotationSpeed;
            transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }
    }
}
