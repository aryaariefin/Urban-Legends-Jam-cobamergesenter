using UnityEngine;
using System.Collections;

public class Door : InteractableObject
{
    public string doorID;
    public bool isLocked = false;
    public bool isOpen = false;

    public Transform pivot; // Assign ke DoorPivot (child)
    public Transform doorVisual; // Assign ke Red Doorz (child)
    public float openAngle = 90f;
    public float rotationSpeed = 180f;

    private Coroutine currentCoroutine;
    private float uiHideDelay = 2f; // detik
    private bool isAnimating = false; // proteksi spam

    protected override void Start()
    {
        base.Start();
        // Update teks interaksi berdasarkan status pintu saat game dimulai
        UpdateInteractionText();
    }

    void UpdateInteractionText()
    {
        if (isLocked)
        {
            interactText = "Pintu terkunci";
        }
        else if (isOpen)
        {
            interactText = "Tekan E untuk menutup pintu";
        }
        else
        {
            interactText = "Tekan E untuk membuka pintu";
        }

        // Refresh UI dengan teks baru
        RefreshInteractionUI();
    }

    public override void Interact()
    {
        if (isAnimating) return;
        if (isLocked)
        {
            Debug.Log("Pintu terkunci. Gunakan kunci yang cocok.");
            return;
        }

        if (isOpen)
        {
            CloseDoor(); // ← jika sudah terbuka, tutup
        }
        else
        {
            OpenDoor(); // ← jika tertutup, buka
        }
    }

        public void TryOpenWithKey(string keyTargetID)
    {
        if (isLocked && keyTargetID == doorID)
        {
            isLocked = false;
            Debug.Log("Kunci cocok. Pintu sekarang bisa dibuka!");
            UpdateInteractionText();
            // Jangan langsung OpenDoor();
        }
        else
        {
            Debug.Log("Kunci tidak cocok.");
        }
    }

    void OpenDoor()
    {
        Debug.Log("Membuka pintu...");
        isOpen = true;
        UpdateInteractionText();
        HideInteractionUIWithDelay(uiHideDelay);
        StartRotate(openAngle);
    }

    void CloseDoor()
    {
        Debug.Log("Menutup pintu...");
        isOpen = false;
        UpdateInteractionText();
        HideInteractionUIWithDelay(uiHideDelay);
        StartRotate(-openAngle);
    }

    void StartRotate(float targetAngle)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        isAnimating = true;
        currentCoroutine = StartCoroutine(RotateDoor(targetAngle));
    }

    IEnumerator RotateDoor(float angle)
    {
        if (doorVisual == null || pivot == null)
        {
            isAnimating = false;
            yield break;
        }

        float rotated = 0f;
        float direction = Mathf.Sign(angle);

        while (Mathf.Abs(rotated) < Mathf.Abs(angle))
        {
            float step = rotationSpeed * Time.deltaTime * direction;
            if (Mathf.Abs(rotated + step) > Mathf.Abs(angle))
                step = angle - rotated;

            doorVisual.RotateAround(pivot.position, Vector3.forward, step);
            rotated += step;
            yield return null;
        }
        isAnimating = false;
    }
}
