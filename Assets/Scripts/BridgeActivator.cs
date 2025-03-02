using TMPro;
using UnityEngine;

public class BridgeActivator : MonoBehaviour
{
    public GameObject destroyedBridge;
    public GameObject bridge;
    public GameObject player;
    public float activationDistance = 15f;
    public Inventory playerInventory;
    public TextMeshPro messageText;
    public CaveVisibilityManager caveVisibilityManager;

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, destroyedBridge.transform.position);

        if (distance <= activationDistance)
        {
            if (playerInventory.HasItem("Plank1") && playerInventory.HasItem("Plank2") && playerInventory.HasItem("Plank3"))
            {
                destroyedBridge.SetActive(false);
                bridge.SetActive(true);

                Vector3 lastScanPosition = caveVisibilityManager.GetLastScanPosition();
                if (lastScanPosition != Vector3.zero)
                {
                    caveVisibilityManager.RevealArea(lastScanPosition, caveVisibilityManager.scanRadius);
                }

                messageText.gameObject.SetActive(false);
            }
            else
            {
                messageText.gameObject.SetActive(true);
                messageText.text = "You need 3 planks to cross the bridge.";
            }
        }
        else
        {
            messageText.gameObject.SetActive(false);
        }
    }
}

