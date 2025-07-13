using UnityEngine;

public class Locker : InteractableObject
{
    public Transform insidePoint;
    public Transform exitPoint;

    private bool playerInside = false;
    private int originalPlayerLayer;
    

    protected override void Start()
    {
        base.Start();
        UpdateInteractionText();
    }

    public override void Interact()
    {
        if (playerInside)
        {
            Exit();
        }
        else
        {
            // Cek radius interaksi hanya saat ingin masuk
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float dist = Vector2.Distance(player.transform.position, transform.position);
            if (dist < interactionRadius)
                Enter();
        }
    }

    void Enter()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = insidePoint.position;

        var controller = player.GetComponent<PlayerMovement>();
        controller.isHiding = true;
        controller.enabled = false; // matikan kontrol

        // Matikan Rigidbody2D player
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        playerInside = true;
        Debug.Log("Player masuk loker 🫣");
        UpdateInteractionText();
    }

    void Exit()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = exitPoint.position;

        var controller = player.GetComponent<PlayerMovement>();
        controller.isHiding = false;
        controller.enabled = true;

        // Aktifkan kembali Rigidbody2D player
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = true;

        playerInside = false;
        Debug.Log("Player keluar dari loker 👀");
        UpdateInteractionText();
    }

    void UpdateInteractionText()
    {
        if (playerInside)
            interactText = "Tekan E untuk keluar dari loker";
        else
            interactText = "Tekan E untuk masuk ke loker";

        RefreshInteractionUI();
    }

    public bool IsPlayerInside() => playerInside;
}