using UnityEngine;
using UnityEngine.InputSystem;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject equippedItem;

    public void equipItem(GameObject newEquip)
    {
        equippedItem = newEquip;
        newEquip.GetComponent<IWeapon>()?.Equip(gameObject); //Equips the weapon if it has IWeapon interface
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
