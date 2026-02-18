using UnityEngine;

public class Camera : MonoBehaviour
{
    public float currentRotation = 0;
    public float rotationSpeed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRotation > 90)
            currentRotation = 90;
        if (currentRotation < -90)
            currentRotation = -90;

    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Mouse Y") < 0 && currentRotation >= -90)
        {
            currentRotation = currentRotation + rotationSpeed;
            transform.localRotation = Quaternion.Euler(currentRotation, 0, 0);
        }
        if (Input.GetAxis("Mouse Y") > 0 && currentRotation <= 90)
        {
            currentRotation = currentRotation - rotationSpeed;
            transform.localRotation = Quaternion.Euler(currentRotation, 0, 0);
        }
    }
}
