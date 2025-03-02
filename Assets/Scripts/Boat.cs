using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : MonoBehaviour
{
    public Material hoverMaterial;
    private Material[] originalMaterials;  
    private Renderer[] boatRenderers;  
    private bool isHovered = false; 

    public float maxInteractionDistance = 5f;

    void Start()
    {
        boatRenderers = GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[boatRenderers.Length];
        for (int i = 0; i < boatRenderers.Length; i++)
        {
            originalMaterials[i] = boatRenderers[i].material;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (IsMouseOverBoat())
            {
                SceneManager.LoadScene("EndScene");
            }
        }

        if (IsMouseOverBoat())
        {
            if (!isHovered)
            {
                HighlightBoat(true);
            }
        }
        else
        {
            if (isHovered)
            {
                HighlightBoat(false);
            }
        }
    }

    private bool IsMouseOverBoat()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject && hit.distance <= maxInteractionDistance)
            {
                return true;
            }
        }
        return false;
    }

    private void HighlightBoat(bool highlight)
    {
        if (highlight)
        {
            for (int i = 0; i < boatRenderers.Length; i++)
            {
                boatRenderers[i].material = hoverMaterial;
            }
            isHovered = true;
        }
        else
        {
            for (int i = 0; i < boatRenderers.Length; i++)
            {
                boatRenderers[i].material = originalMaterials[i];
            }
            isHovered = false;
        }
    }
}
