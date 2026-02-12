using UnityEngine;

public class Player : MonoBehaviour
{
    int playerSpeed = 5;
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
            rb.linearVelocity = new Vector3((playerSpeed / Mathf.Sqrt(2)) * horizontal, rb.linearVelocity.y, (playerSpeed / Mathf.Sqrt(2)) * vertical);
        }
        else 
        {
            rb.linearVelocity = new Vector3(playerSpeed * horizontal, rb.linearVelocity.y, playerSpeed * vertical);
        }
    }
}
