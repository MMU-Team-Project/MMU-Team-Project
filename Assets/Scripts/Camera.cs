using UnityEngine;

public class Camera : MonoBehaviour
{
    public float currentRotation = 0;
    public float currentXRotation= 0;
    public float rotationSpeed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRotation > 70)
            currentRotation = 70;
        if (currentRotation < -70)
            currentRotation = -70;
        if (Input.GetAxis("Mouse Y") < 0 && currentRotation >= -70)
        {
            currentRotation = currentRotation - rotationSpeed * Input.GetAxis("Mouse Y");
            transform.localRotation = Quaternion.AngleAxis(currentRotation, Vector3.right);
        }
        if (Input.GetAxis("Mouse Y") > 0 && currentRotation <= 70)
        {
            currentRotation = currentRotation - rotationSpeed * Input.GetAxis("Mouse Y");
            transform.localRotation = Quaternion.AngleAxis(currentRotation, Vector3.right);
        }
        //if (Input.GetAxis("Mouse X") < 0)
        //{
        //    currentXRotation = currentXRotation + rotationSpeed * Input.GetAxis("Mouse X");
        //    transform.rotation = Quaternion.AngleAxis(currentXRotation, Vector3.up);
        //}
        //if (Input.GetAxis("Mouse X") > 0)
        //{
        //    currentXRotation = currentXRotation + rotationSpeed * Input.GetAxis("Mouse X");
        //    transform.rotation = Quaternion.AngleAxis(currentXRotation, Vector3.up);
        //}
    }
    //void FixedUpdate()
    //{
    //    if (Input.GetAxis("Mouse Y") < 0 && currentRotation >= -70)
    //    {
    //        currentRotation = currentRotation - rotationSpeed * Input.GetAxis("Mouse Y");
    //        transform.localRotation = Quaternion.AngleAxis(currentRotation,Vector3.right);
    //    }
    //    if (Input.GetAxis("Mouse Y") > 0 && currentRotation <= 70)
    //    {
    //        currentRotation = currentRotation - rotationSpeed * Input.GetAxis("Mouse Y");
    //        transform.localRotation = Quaternion.AngleAxis(currentRotation,Vector3.right);
    //    }
    //}
}
