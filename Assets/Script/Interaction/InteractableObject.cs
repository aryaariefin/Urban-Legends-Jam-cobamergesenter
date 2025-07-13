using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Untuk komponen Text
using TMPro; // Untuk komponen TMP_Text

public class InteractableObject : MonoBehaviour
{
    [Header("Interaction Settings")]
    public string interactText = "Tekan E untuk berinteraksi";
    public float interactionRadius = 1.5f; // radius interaksi bisa diatur di Inspector

    private bool isPlayerNearby = false; // status tracking
    private Coroutine hideUICoroutine;

    protected virtual void Start()
    {
        // No need to manage per-object UI anymore
    }

    public virtual void Interact()
    {
        Debug.Log($"Interaksi dengan objek: {gameObject.name}");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.transform.position);
        bool inRange = dist < interactionRadius;

        // Deteksi perubahan status
        if (inRange != isPlayerNearby)
        {
            isPlayerNearby = inRange;
            ShowInteractionUI(isPlayerNearby);
        }
    }

    void ShowInteractionUI(bool show)
    {
        // Daftarkan ke InteractionController untuk UI overlay
        if (InteractionController.Instance != null)
        {
            InteractionController.Instance.RegisterObject(this, show);
        }
        if (show)
        {
            Debug.Log($"🔔 {interactText} - {gameObject.name}");
        }
        else
        {
            Debug.Log($"❌ Meninggalkan area interaksi - {gameObject.name}");
        }
    }

    // Method untuk refresh UI interaksi (dipanggil saat teks berubah)
    public void RefreshInteractionUI()
    {
        // No per-object UI to refresh
    }

    // Hide UI for a delay, then show again if player still in range
    public void HideInteractionUIWithDelay(float delay)
    {
        // No per-object UI to hide
    }

    private IEnumerator HideUIRoutine(float delay)
    {
        yield break;
    }
}