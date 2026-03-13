using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemSlot : MonoBehaviour, IDropHandler
{

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

    private void MoveItemToEmptySlot(GameObject inventoryItem)
    {
        DragHandler dragHandler = inventoryItem.GetComponent<DragHandler>();

        if (dragHandler != null)
        {
            dragHandler.TargetTransform = transform;
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
}
