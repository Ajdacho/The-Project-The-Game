using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    public GameObject scanner;
    public ScannerController scannerController;

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log($"Added {item.name} to inventory.");
        if (item.name == "Scanner")
        {
            scannerController = new ScannerController(scanner);
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
}