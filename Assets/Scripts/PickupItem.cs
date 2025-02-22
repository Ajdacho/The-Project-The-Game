using UnityEngine;


public class PickupItem : MonoBehaviour
{
    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer renderer;
    private Renderer[] renderers;

    AudioManager AudioManager;

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            originalMaterial = renderers[0].material;
        }

        if (!CompareTag("Pickupable"))
        {
            Debug.LogWarning($"Object {gameObject.name} does not have the 'Pickupable' tag!");
        }
    }

    private void OnMouseEnter()
    {
        if (renderers != null && highlightMaterial != null)
        {
            foreach (Renderer r in renderers)
            {
                r.material = highlightMaterial;
            }
        }
    }
    private void OnMouseExit()
    {
        if (renderers != null)
        {
            foreach (Renderer r in renderers)
            {
                r.material = originalMaterial;
            }
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
            AudioManager.PlaySFX(AudioManager.Audio_Pickup);
        }
        else
        {
            Debug.LogError("Inventory system not found in the scene!");
        }
    }
}
//>>>>>>> 03e551f (Dodanie plików audio / efektów dźwiękowych / ambientu w tle)
