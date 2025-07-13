using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(ItemData newItem)
    {
        items.Add(newItem);
        Debug.Log("Item ditambahkan: " + newItem.itemName);
    }
    public void UseItem(ItemData itemToUse)
    {
        if (!items.Contains(itemToUse)) return;

        if (itemToUse.itemType == ItemType.Key)
        {
            Collider2D hit = Physics2D.OverlapCircle(
                GameObject.FindWithTag("Player").transform.position,
                2f, // jarak interaksi ke pintu
                LayerMask.GetMask("Interactables") // pastikan pintu ada di layer ini
            );

            if (hit)
            {
                Door door = hit.GetComponent<Door>();
                if (door != null)
                {
                    door.TryOpenWithKey(itemToUse.keyTargetID);
                }
            }
        }
    }

    public void UseKeyNearPlayer()
    {
        var player = GameObject.FindWithTag("Player").transform;

        Collider2D hit = Physics2D.OverlapCircle(
            player.position,
            2f,
            LayerMask.GetMask("Interactables")
        );

        if (hit)
        {
            Door door = hit.GetComponent<Door>();
            if (door != null && door.isLocked)
            {
                // Cari kunci yang cocok
                ItemData matchingKey = items.Find(item =>
                    item.itemType == ItemType.Key &&
                    item.keyTargetID == door.doorID);

                if (matchingKey != null)
                {
                    door.TryOpenWithKey(matchingKey.keyTargetID);
                }
                else
                {
                    Debug.Log("🚫 Tidak ada kunci yang cocok di inventory.");
                }
            }
        }
    }
}