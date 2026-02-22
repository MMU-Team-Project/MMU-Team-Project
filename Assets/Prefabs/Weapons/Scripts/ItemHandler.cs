using UnityEngine;
using UnityEngine.InputSystem;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject equippedItem;

    public void equipItem(GameObject newEquip)
    {
        if (equippedItem != null) //Prevents multiple of the same item being equipped
            unequipItem();
        equippedItem = newEquip;
        if (equippedItem != null) //Check if what is attempting to be equipped exists
        {
            newEquip.GetComponent<IWeapon>()?.Equip(gameObject); //Equips the weapon if it has IWeapon interface
        }
        else
        {
            unequipItem();
        }
    }
    public void unequipItem()
    {
        if (equippedItem != null) //Check if something is already equipped
        {
            Debug.Log("Unequipping");
            Destroy(equippedItem);
        }
    }

    public void useItem(InputAction.CallbackContext context)
    {
        if (context.performed && equippedItem != null)
        {
            equippedItem?.GetComponent<IWeapon>()?.Use();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
