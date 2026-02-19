using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Equip : MonoBehaviour
{
    bool equipped = false;
    ItemHandler items;
    [SerializeField] private GameObject[] inventory = new GameObject[10];

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
            int equipSlot = selectedSlot.keyCode == Key.Digit0 ? 9 : (int)(selectedSlot.keyCode - Key.Digit1); //Handles conversion of NumKey input into Integer in one line, to be concise.
            GameObject equipGoal = inventory[equipSlot];
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
}
