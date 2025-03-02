using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer[] renderers;
    private Transform player;
    public float pickupRange = 5f;

    AudioManager AudioManager;

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0 && renderers[0] != null)
        {
            originalMaterial = renderers[0].material;
        }

        if (!CompareTag("Pickupable"))
        {
            Debug.LogWarning($"Object {gameObject.name} does not have the 'Pickupable' tag!");
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > pickupRange)
        {
            ResetMaterial();
        }
    }

    private void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, player.position) <= pickupRange && highlightMaterial != null)
        {
            foreach (Renderer r in renderers)
            {
                r.material = highlightMaterial;
            }
        }
    }

    private void OnMouseExit()
    {
        ResetMaterial();
    }

    private void ResetMaterial()
    {
        if (renderers != null)
        {
            foreach (Renderer r in renderers)
            {
                r.material = originalMaterial;
            }
        }
    }

    public void Pickup()
    {
        if (!CompareTag("Pickupable"))
        {
            return;
        }

        if (Vector3.Distance(transform.position, player.position) > pickupRange)
        {
            return;
        }

        AudioManager.PlaySFX(AudioManager.Audio_Pickup);

        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }

        Note note = gameObject.GetComponent<Note>();
        if (note != null)
        {
            note.OpenNote();
        }
    }
}