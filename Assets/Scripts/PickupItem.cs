using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer[] renderers;
    private AudioManager AudioManager;

    public float pickupRange = 5f;

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        // Sprawdzenie, czy gracz jest w zasiêgu i podœwietlenie
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= pickupRange)
        {
            // Aktywowanie efektu hover
            OnMouseEnter();

            // Sprawdzenie, czy klikniêto przycisk do podniesienia przedmiotu
            if (Input.GetMouseButtonDown(0))  // Lewy przycisk myszy
            {
                Pickup();
            }
        }
        else
        {
            // Wy³¹czenie efektu hover, jeœli gracz jest poza zasiêgiem
            OnMouseExit();
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

    public void Pickup()
    {
        if (!CompareTag("Pickupable"))
        {
            Debug.LogError($"Object {gameObject.name} does not have the 'Pickupable' tag!");
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
            Debug.LogError("Inventory system not found in the scene!");
            return;
        }

        Note note = gameObject.GetComponent<Note>();
        if (note != null)
        {
            note.OpenNote();
        }
    }
}
