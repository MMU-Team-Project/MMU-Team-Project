using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform targetTransform;

    [SerializeField]
    private Image targetImage;
    
    public Transform TargetTransform
    {
        get {return targetTransform;}
        set {targetTransform = value;}
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging started");
        targetTransform = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        targetImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging now");
        Vector3 newPos = Input.mousePosition;
        newPos.x = Mathf.Clamp(newPos.x, 0, Screen.width);
        newPos.y = Mathf.Clamp(newPos.y, 0, Screen.height);
        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging ended");
        transform.SetParent(targetTransform);
        targetImage.raycastTarget = true;
    }
}
