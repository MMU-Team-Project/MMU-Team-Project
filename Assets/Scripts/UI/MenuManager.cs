using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject InventoryUI;
    [SerializeField]
    private GameObject Backdrop;

    [SerializeField]
    private bool inventoryOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if(inventoryOpen)
            {
                CloseInventory();
                inventoryOpen = false;
            }
            else
            {
                OpenInventory();
                inventoryOpen = true;
            }
        }
        
    }

    void OpenInventory()
    {
        InventoryUI.SetActive(true);
        Backdrop.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void CloseInventory()
    {
        InventoryUI.SetActive(false);
        Backdrop.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
