using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public GameObject itemPickup;
}
