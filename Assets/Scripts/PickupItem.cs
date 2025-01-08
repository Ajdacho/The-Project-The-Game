using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
        }

        if (!CompareTag("Pickupable"))
        {
            Debug.LogWarning($"Object {gameObject.name} does not have the 'Pickupable' tag!");
        }
    }

    private void OnMouseEnter()
    {
        if (renderer != null && highlightMaterial != null)
        {
            renderer.material = highlightMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (renderer != null)
        {
            renderer.material = originalMaterial;
        }
    }

    private void OnMouseDown()
    {
        if (!CompareTag("Pickupable"))
        {
            Debug.LogError($"Object {gameObject.name} does not have the 'Pickupable' tag!");
            return;
        }

        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(gameObject);
            gameObject.SetActive(false);
            Debug.Log($"Picked up {gameObject.name}!");
        }
        else
        {
            Debug.LogError("Inventory system not found in the scene!");
        }
    }
}
