using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    public ScannerController scannerController;

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log($"Added {item.name} to inventory.");
        if (item.name == "Scanner")
        {
            scannerController = new ScannerController();
            scannerController.ActivateScanner();
        }
    }

    public bool HasItem(string itemName)
    {
        foreach (GameObject item in items)
        {
            if (item.name == itemName)
                return true;
        }
        return false;
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == itemName)
            {
                Debug.Log($"Removed {items[i].name} from inventory.");
                items.RemoveAt(i);
                return;
            }
        }
        Debug.LogWarning($"Item {itemName} not found in inventory.");
    }
}
