using UnityEngine;

public class Equip : MonoBehaviour
{
    bool equipped = false;
    [SerializeField] private GameObject objStaff;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && equipped == false) //Staff equip, Alpha3 = Number key 3
        {
            Debug.Log("Equip the staff");
            GameObject staffModel = Instantiate(objStaff);
            staffModel.transform.parent = transform;
            staffModel.transform.localPosition = offset;
            staffModel.transform.localRotation = Quaternion.Euler(rotation);
            Staff staffScript = staffModel.GetComponent<Staff>();
            staffScript.SetPlayer(gameObject);
            equipped = true;
        }
    }
}
