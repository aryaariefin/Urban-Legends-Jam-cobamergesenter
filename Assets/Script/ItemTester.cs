using UnityEngine;

public class ItemTester : MonoBehaviour
{
    public ItemData testKey;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) // misal tombol 'U' untuk pakai item
        {
            InventorySystem.Instance.UseItem(testKey);
        }
    }
}