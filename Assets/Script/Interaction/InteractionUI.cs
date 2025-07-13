using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject interactionPanel; // Panel overlay
    public Text interactionText; // Komponen Text biasa
    public TMP_Text interactionTMPText; // Komponen TextMeshPro

    [Header("Text Format")]
    public string textFormat = "Tekan E untuk berinteraksi dengan {0}";

    private static InteractionUI instance;
    public static InteractionUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<InteractionUI>();
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        HideInteractionText();
    }

    public void ShowInteractionText(string objectName)
    {
        string displayText = string.Format(textFormat, objectName);
        if (interactionPanel != null)
            interactionPanel.SetActive(true);
        if (interactionText != null)
            interactionText.text = displayText;
        if (interactionTMPText != null)
            interactionTMPText.text = displayText;
    }

    public void ShowCustomText(string customText)
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(true);
        if (interactionText != null)
            interactionText.text = customText;
        if (interactionTMPText != null)
            interactionTMPText.text = customText;
    }

    public void HideInteractionText()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);
    }
}