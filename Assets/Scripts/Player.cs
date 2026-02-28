using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5;
    public float jumpSpeed = 0;
    public float rotationSpeed = 10;
    public float currentRotation = 0;
    public bool grounded = true;
    public bool jumping = false;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && (grounded||jumping))
        {
            rb.linearVelocity += transform.up * jumpSpeed;
            jumping = true;
            grounded = false;
        }
        if (!Input.GetButtonDown("Jump"))
            jumping = false;
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0) //corrects diagonal speed
        {
            rb.linearVelocity = transform.forward * vertical * playerSpeed / Mathf.Sqrt(2) + transform.right * horizontal * playerSpeed / Mathf.Sqrt(2) + transform.up * rb.linearVelocity.y;
        }
        else
        {
            rb.linearVelocity = transform.forward * vertical * playerSpeed + transform.right * horizontal * playerSpeed + transform.up * rb.linearVelocity.y;
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            currentRotation=currentRotation - rotationSpeed;
            transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            currentRotation = currentRotation + rotationSpeed;
            transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
        }
        if (rb.linearVelocity.y > 0 && !grounded)
        {
            rb.linearVelocity += transform.up * 1.2f * Time.deltaTime * Physics.gravity.y;
        }
        if (rb.linearVelocity.y < 0 && !grounded)
        {
            rb.linearVelocity += transform.up * jumpSpeed * Time.deltaTime * Physics.gravity.y;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = true; //checks if on the ground
        }
    }
}
