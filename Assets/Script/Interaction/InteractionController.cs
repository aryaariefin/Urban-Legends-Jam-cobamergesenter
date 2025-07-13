using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InteractionController : MonoBehaviour
{
    private static InteractionController instance;
    public static InteractionController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<InteractionController>();
                if (instance == null)
                {
                    GameObject go = new GameObject("InteractionController");
                    instance = go.AddComponent<InteractionController>();
                }
            }
            return instance;
        }
    }

    private List<InteractableObject> nearbyObjects = new List<InteractableObject>();
    private InteractableObject currentClosest;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UpdateClosestObject();
    }

    public void RegisterObject(InteractableObject obj, bool isNearby)
    {
        if (isNearby)
        {
            if (!nearbyObjects.Contains(obj))
            {
                nearbyObjects.Add(obj);
            }
        }
        else
        {
            nearbyObjects.Remove(obj);
        }
    }

    private void UpdateClosestObject()
    {
        // Bersihkan objek yang sudah null/destroyed
        nearbyObjects = nearbyObjects.Where(obj => obj != null).ToList();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || nearbyObjects.Count == 0)
        {
            if (currentClosest != null)
            {
                currentClosest = null;
                if (InteractionUI.Instance != null)
                {
                    InteractionUI.Instance.HideInteractionText();
                }
            }
            return;
        }

        // Cari objek terdekat
        InteractableObject closest = nearbyObjects
            .OrderBy(obj => Vector2.Distance(obj.transform.position, player.transform.position))
            .FirstOrDefault();

        if (closest != currentClosest)
        {
            currentClosest = closest;
            if (currentClosest != null && InteractionUI.Instance != null)
            {
                // Only pass the name for screen-space UI
                InteractionUI.Instance.ShowInteractionText(currentClosest.gameObject.name);
            }
        }
    }

    public InteractableObject GetCurrentClosest()
    {
        return currentClosest;
    }
}