using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveVisibilityManager : MonoBehaviour
{
    public Material scannableMaterial; 
    public float scanRadius = 9f; 
    public float scanFadeDuration = 1f;
    public float scanCooldown = 2f;

    private Transform player;
    private Inventory playerInventory;
    private float lastScanTime = -Mathf.Infinity;

    AudioManager AudioManager;
    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            playerInventory = player.GetComponent<Inventory>();
        }

        if (player == null)
        {
            Debug.LogError("Player object with tag 'Player' not found in the scene.");
            return;
        }

        if (playerInventory == null)
        {
            Debug.LogError("Player object is missing the 'Inventory' component.");
            return;
        }

        Debug.Log("Player and Inventory successfully initialized.");

        Collider[] colliders = Physics.OverlapSphere(player.position, scanRadius);
        foreach (Collider col in colliders)
        {
            Renderer renderer = col.GetComponent<Renderer>();
            if (renderer != null)
            {
                foreach (var mat in renderer.materials)
                {
                    if (mat.shader == scannableMaterial.shader)
                    {
                        mat.SetFloat("_ScanRadius", 0f);
                        mat.SetVector("_ScanCenter", new Vector4(player.position.x, player.position.y, player.position.z, 1));
                        Debug.Log($"Initialized material {mat.name} for {renderer.name}.");
                    }
                }
            }
        }

        RevealArea(player.position, scanRadius);
    }

    void Update()
    {
        if (player == null || playerInventory == null) return;

        if (Input.GetMouseButtonDown(0) && PlayerHasScanner())
        {
            if (Time.time >= lastScanTime + scanCooldown)
            {

                if (Camera.main == null)
                {
                    Debug.LogError("Main Camera not found in the scene.");
                    return;
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log($"Revealing area at {hit.point}.");
                    RevealArea(hit.point, scanRadius);
                    lastScanTime = Time.time;
                }
                else
                {
                    Debug.LogWarning("Raycast did not hit any collider.");
                }
            }
            else
            {
                Debug.Log("Scan is on cooldown!");
            }
        }
    }
    private bool PlayerHasScanner()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory is null during PlayerHasScanner check.");
            return false;
        }

        bool hasScanner = playerInventory.HasItem("Scanner");
        Debug.Log($"PlayerHasScanner check: {hasScanner}");
        return hasScanner;
    }

    private void RevealArea(Vector3 position, float radius)
    {
        ResetScannableObjects();
        AudioManager.PlaySFX(AudioManager.Audio_Scanner);
        Debug.Log($"Revealing area at {position} with radius {radius}.");

        Collider[] colliders = Physics.OverlapSphere(position, radius);
        if (colliders.Length == 0)
        {
            Debug.LogWarning("No colliders found in the scan area.");
            return;
        }

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
                continue;

            Renderer renderer = col.GetComponent<Renderer>();
            if (renderer == null)
                continue;

            Debug.Log($"Processing renderer: {renderer.name}");

            foreach (var mat in renderer.materials)
            {
                if (mat.shader == scannableMaterial.shader)
                {
                    mat.SetVector("_ScanCenter", new Vector4(position.x, position.y, position.z, 1));
                    mat.SetFloat("_ScanRadius", radius);

                    Debug.Log($"Set _ScanCenter to {position} and _ScanRadius to {radius} on material {mat.name}.");
                    StartCoroutine(FadeMaterialRadius(mat, radius, scanFadeDuration));
                }
            }

        }
    }
    private void ResetScannableObjects()
    {
        Renderer[] allRenderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in allRenderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.shader == scannableMaterial.shader)  // Resetujemy tylko materiały niewidoczne
                {
                    mat.SetFloat("_ScanRadius", 0f);
                    mat.SetVector("_ScanCenter", Vector4.zero);
                }
            }
        }
    }

    private IEnumerator FadeMaterialRadius(Material material, float targetRadius, float duration)
    {
        if (!material.HasProperty("_ScanRadius"))
        {
            Debug.LogError($"Material '{material.name}' does not have the '_ScanRadius' property.");
            yield break;
        }

        float currentRadius = material.GetFloat("_ScanRadius");
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newRadius = Mathf.Lerp(currentRadius, targetRadius, elapsedTime / duration);
            material.SetFloat("_ScanRadius", newRadius);
            yield return null;
        }

        material.SetFloat("_ScanRadius", targetRadius);
    }
}