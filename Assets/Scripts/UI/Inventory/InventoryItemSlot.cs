using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour, IDropHandler
{
    public SlotType slotType;
    public GameObject HotbarParent;

    public GameObject player;

    Transform targetParent;

    public int hotbarNum;

    public GameObject[] hotBar;

    void Awake()
    {
        hotBar = player.GetComponent<EquipEdited>().hotBar;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggingEndedItem = eventData.pointerDrag;

        if (draggingEndedItem != null)
        {
            if (IsSlotEmpty())
            {
                MoveItemToEmptySlot(draggingEndedItem);
            }
            else
            {
                SwapItems(draggingEndedItem);
            }
        }
    }

    private bool IsSlotEmpty()
    {
        return transform.childCount == 0;
    }

    void MoveItemToEmptySlot(GameObject inventoryItem)
    {
        DragHandler dragHandler = inventoryItem.GetComponent<DragHandler>();


        if (dragHandler != null)
        {
            dragHandler.TargetTransform = transform;

            if (slotType == SlotType.Hotbar)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (hotbarNum == i)
                    {
                        targetParent = HotbarParent.transform.GetChild(i);
                        Instantiate(inventoryItem, targetParent);
                        hotBar[i] = inventoryItem.GetComponent<Item>().itemData.itemPrefab;
                    }
                }
            }
        }
    }

    private void SwapItems(GameObject draggingEndedItem)
    {
        GameObject existingItem = transform.GetChild(0).gameObject;
        DragHandler existingItemDragHandler = existingItem.GetComponent<DragHandler>();
        DragHandler draggingEndedItemDragHandler = draggingEndedItem.GetComponent<DragHandler>();

        existingItem.transform.SetParent(draggingEndedItemDragHandler.TargetTransform);
        draggingEndedItem.transform.SetParent(transform);
        existingItemDragHandler.TargetTransform = draggingEndedItemDragHandler.TargetTransform;
        draggingEndedItemDragHandler.TargetTransform = transform;
    }

    public void ClearSlot()
    {
        Debug.Log("bing bong");
        if (slotType == SlotType.Hotbar)
        {
            for (int i = 0;i < 4;i++)
            {
                if (hotbarNum == i)
                {
                    targetParent = HotbarParent.transform.GetChild(i);
                    foreach (Transform child in targetParent)
                    {
                        hotBar[i] = null;
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }
        }
    }
}

public enum SlotType {None, Hotbar};
