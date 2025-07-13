using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 1.5f;
    public LayerMask interactLayer;
    private InteractableObject currentTarget;

    void Update()
    {
        CheckForInteractable();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentTarget != null)
            {
                // Cek apakah target adalah pintu
                Door door = currentTarget.GetComponent<Door>();
                if (door != null)
                {
                    if (door.isLocked)
                    {
                        // Cek apakah ada kunci yang cocok
                        var matchingKey = InventorySystem.Instance.items.Find(item =>
                            item.itemType == ItemType.Key &&
                            item.keyTargetID == door.doorID);

                        if (matchingKey != null)
                        {
                            Debug.Log("🔑 Kunci cocok ditemukan, membuka pintu...");
                            door.TryOpenWithKey(matchingKey.keyTargetID);
                        }
                        else
                        {
                            Debug.Log("🚫 Tidak punya kunci yang cocok untuk pintu ini.");
                        }
                    }
                    else
                    {
                        // Pintu tidak terkunci, langsung interaksi (toggle buka/tutup)
                        door.Interact();
                    }
                }
                else
                {
                    // Kalau objek bukan pintu, interaksi biasa
                    currentTarget.Interact();
                }
            }
            else
            {
                Debug.Log("Tidak ada objek di dekat untuk interaksi.");
            }
        }

        // Tombol Q → cek apakah punya kunci
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            var keyItems = InventorySystem.Instance.items.FindAll(item => item.itemType == ItemType.Key);

            if (keyItems.Count == 0)
            {
                Debug.Log("❌ Player belum punya kunci apa pun.");
            }
            else
            {
                Debug.Log("🔑 Player punya kunci:");
                foreach (var key in keyItems)
                {
                    Debug.Log("→ " + key.itemName + " | Target: " + key.keyTargetID);
                }
            }
        }
    }

    void CheckForInteractable()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, interactLayer);
        if (hit != null)
        {
            currentTarget = hit.GetComponent<InteractableObject>();
            // bisa tambahkan debug di sini
        }
        else
        {
            currentTarget = null;
        }
    }


}