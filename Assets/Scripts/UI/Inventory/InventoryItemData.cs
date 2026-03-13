using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
}
