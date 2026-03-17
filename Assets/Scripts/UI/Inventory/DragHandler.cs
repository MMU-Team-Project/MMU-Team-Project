using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform targetTransform;

    public Image targetImage;
    
    public Transform TargetTransform
    {
        get {return targetTransform;}
        set {targetTransform = value;}
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        targetTransform = transform.parent;
        GetComponentInParent<InventoryItemSlot>().ClearSlot();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        targetImage.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = Input.mousePosition;
        newPos.x = Mathf.Clamp(newPos.x, 0, Screen.width);
        newPos.y = Mathf.Clamp(newPos.y, 0, Screen.height);
        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(targetTransform);
        targetImage.raycastTarget = true;
    }
}
