using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Equip : MonoBehaviour
{
    bool equipped = false;
    [SerializeField] private GameObject objStaff;
    
    [SerializeField] private GameObject[] inventory = new GameObject[10];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha3) && equipped == false) //Staff equip, Alpha3 = Number key 3
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
        */
    }

    public void EquipWep(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var selectedSlot = context.control as KeyControl;
            int equipSlot = selectedSlot.keyCode == Key.Digit0 ? 9 : (int)(selectedSlot.keyCode - Key.Digit1); //Handles conversion of NumKey input into Integer in one line, to be concise.
            GameObject equipGoal = inventory[equipSlot];
            if (equipGoal != null)
            {
                equipped = true;
                GameObject wepModel = Instantiate(equipGoal);
                wepModel.GetComponent<IWeapon>()?.Equip(gameObject); //Equips the weapon if it has IWeapon interface
            }
            else
            {
                equipped = false;
            }
            Debug.Log(equipped);
        }
    }
}
