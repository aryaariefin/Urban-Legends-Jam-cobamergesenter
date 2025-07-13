using UnityEngine;

public enum ItemType { Key, Usable, Lore }

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;
    [TextArea] public string description;
    public string keyTargetID; // ← tambahkan ini
}