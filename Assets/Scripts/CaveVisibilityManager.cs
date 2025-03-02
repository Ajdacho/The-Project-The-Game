using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveVisibilityManager : MonoBehaviour
{
    public Material scannableMaterial;
    public float scanRadius = 9f;
    public float scanFadeDuration = 1f;
    public float scanCooldown = 2f;
    public bool firstScan = true;
    public PauseMenu PauseMenu;

    private Transform player;
    private Inventory playerInventory;
    private float lastScanTime = -Mathf.Infinity;
    private List<Renderer> scannableRenderers = new List<Renderer>();

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
                    }
                }
            }
        }
        Renderer[] allRenderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in allRenderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.shader == scannableMaterial.shader)
                {
                    scannableRenderers.Add(renderer);
                    break;
                }
            }
        }
        RevealArea(player.position, scanRadius);
    }

    void Update()
    {
        if (player == null || playerInventory == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Pickupable") && !PauseMenu.isPaused)
                {
                    hit.collider.GetComponent<PickupItem>().Pickup();
                    return;
                }

                if (!PlayerHasScanner())
                {
                    Debug.Log("Gracz nie posiada skanera!");
                    return;
                }

                if (Time.time >= lastScanTime + scanCooldown)
                {
                    RevealArea(hit.point, scanRadius);
                    lastScanTime = Time.time;
                }
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
        return hasScanner;
    }

    private void RevealArea(Vector3 position, float radius)
    {
        ResetScannableObjects();

        if (!firstScan)
        {
            AudioManager.PlaySFX(AudioManager.Audio_Scanner);
        }
        firstScan = false;

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

            foreach (var mat in renderer.materials)
            {
                if (mat.shader == scannableMaterial.shader)
                {
                    mat.SetVector("_ScanCenter", new Vector4(position.x, position.y, position.z, 1));
                    StartCoroutine(AnimateScanWave(mat, radius, scanFadeDuration));
                }
            }
        }
    }

    private IEnumerator AnimateScanWave(Material material, float maxRadius, float duration)
    {
        if (!material.HasProperty("_ScanRadius"))
        {
            Debug.LogError($"Material '{material.name}' does not have the '_ScanRadius' property.");
            yield break;
        }

        float elapsedTime = 0f;
        float waveSpeed = 15f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime * waveSpeed;
            float newRadius = Mathf.Lerp(0f, maxRadius, elapsedTime / duration);
            material.SetFloat("_ScanRadius", newRadius);

            if (material.HasProperty("_ScanWave"))
            {
                float waveStrength = Mathf.Sin(elapsedTime * Mathf.PI);
                material.SetFloat("_ScanWave", waveStrength);
            }

            yield return null;
        }

        material.SetFloat("_ScanRadius", maxRadius);
    }


    private void ResetScannableObjects()
    {
        Vector4 resetPosition = new Vector4(0, 100, 0);

        for (int i = scannableRenderers.Count - 1; i >= 0; i--)
        {
            if (scannableRenderers[i] == null)
            {
                scannableRenderers.RemoveAt(i);
                continue;
            }

            foreach (var mat in scannableRenderers[i].materials)
            {
                if (mat.shader == scannableMaterial.shader)
                {
                    mat.SetFloat("_ScanRadius", 0f);
                    mat.SetVector("_ScanCenter", resetPosition);
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
