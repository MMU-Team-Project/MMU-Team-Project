using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class EquipEdited : MonoBehaviour
{
    bool equipped = false;
    ItemHandler items;
    public GameObject[] hotBar = new GameObject[4];
    public int equipSlot;

    void Awake()
    {
        items = transform.GetComponent<ItemHandler>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EquipWep(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var selectedSlot = context.control as KeyControl;
            equipSlot = selectedSlot.keyCode == Key.Digit0 ? 9 : (int)(selectedSlot.keyCode - Key.Digit1); //Handles conversion of NumKey input into Integer in one line, to be concise.
            GameObject equipGoal = hotBar[equipSlot];
            if (equipGoal != null)
            {
                GameObject wepModel = Instantiate(equipGoal);
                items.equipItem(wepModel);
            }
            else
            {
                items.unequipItem();
            }
            Debug.Log(equipped);
        }
    }

    public void EquipCheck()
    {
        GameObject equipGoal = hotBar[equipSlot];
        if (equipGoal != null)
        {
            GameObject wepModel = Instantiate(equipGoal);
            items.equipItem(wepModel);
        }
        else
        {
            items.unequipItem();
        }
        Debug.Log(equipped);
    }
}
