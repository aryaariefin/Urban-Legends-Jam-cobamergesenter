using UnityEngine;
public class CollectableItem : InteractableObject
{
    public ItemData itemData;

    public override void Interact()
    {
        Debug.Log("Item diambil: " + itemData.itemName);
        InventorySystem.Instance.AddItem(itemData);
        Destroy(gameObject); // hilangkan dari scene
    }
}