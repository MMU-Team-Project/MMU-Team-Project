using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5;
    public float playerSprintSpeed = 5;
    public float playerSprintMulti;
    public float jumpSpeed = 0;
    public float rotationSpeed = 10;
    public float currentRotation = 0;
    public bool grounded = true;
    public bool jumping = false;
    public bool walking = false;
    public bool hit = false;
    public float health = 100;
    private bool Hitting = false;
    public GameObject HittingEnemy;
    Animator animator;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //line below results in syntax error; stops rest of the Update() function from running. Disabled for fixes.
        //animator.SetBool("Is Walking", walking);
        if (Input.GetButtonDown("Jump") && (grounded || jumping))
        {
            rb.linearVelocity += transform.up * jumpSpeed;
            jumping = true;
            grounded = false;
        }
        if (!Input.GetButtonDown("Jump"))
            jumping = false;

        if (Input.GetAxis("Mouse X") < 0)
        {
            currentRotation = currentRotation + rotationSpeed * Input.GetAxis("Mouse X");
            transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            currentRotation = currentRotation + rotationSpeed * Input.GetAxis("Mouse X");
            transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
        }
        if (Input.GetKeyDown("left shift"))
        {
            playerSprintMulti = playerSprintSpeed;
        }
        if (Input.GetKeyUp("left shift"))
        {
            playerSprintMulti = 2;
        }
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0) //corrects diagonal speed
        {
            walking = true;
            rb.linearVelocity = transform.forward * vertical * playerSpeed * playerSprintMulti / Mathf.Sqrt(2) + transform.right * horizontal * playerSpeed * playerSprintMulti / Mathf.Sqrt(2) + transform.up * rb.linearVelocity.y;
        }
        else if (horizontal != 0 || vertical != 0)
        {
            walking = true;
            rb.linearVelocity = transform.forward * vertical * playerSpeed * playerSprintMulti + transform.right * horizontal * playerSpeed * playerSprintMulti + transform.up * rb.linearVelocity.y;
        }
        else
        {
            walking = false;
            rb.linearVelocity = transform.forward * vertical * playerSpeed * playerSprintMulti + transform.right * horizontal * playerSpeed * playerSprintMulti + transform.up * rb.linearVelocity.y;
        }
        if (rb.linearVelocity.y > 0 && !grounded)
        {
            rb.linearVelocity += transform.up * 1.2f * Time.deltaTime * Physics.gravity.y;
        }
        if (rb.linearVelocity.y < 0 && !grounded)
        {
            rb.linearVelocity += transform.up * jumpSpeed * Time.deltaTime * Physics.gravity.y;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (hit)
        {
            StartCoroutine(HitByEnemy(hit));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = true; //checks if on the ground
        }
        if (collision.gameObject.tag == "Enemy")
        {
            hit = true;
            HittingEnemy = collision.gameObject;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hit = false;
            StopCoroutine(HitByEnemy(hit));
        }
    }

    IEnumerator HitByEnemy(bool hit)
    {
        if (hit)
        {
            if (Hitting == false)
            {
                Hitting = true;
                float damage = HittingEnemy.GetComponent<EnemyHealth>().Damage;
                health -= damage;
                print("Player hit for " + damage + "health. From " + (health + damage) + " down to " + health);
                yield return new WaitForSeconds(1);
                Hitting = false;
                if (!hit)
                {
                    StopCoroutine(HitByEnemy(hit));

                }
            }
        }
    }
}
