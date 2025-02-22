using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject scanner;
    private GameObject FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child.gameObject;

            GameObject found = FindInChildren(child, name);
            if (found != null)
                return found;
        }
        return null;
    }

    private void Start()
    {
        scanner = FindInChildren(transform, "ScannerHold");
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log($"Added {item.name} to inventory.");
        if (item.name == "Scanner")
        {
            scanner.SetActive(true);
            Debug.Log("Scanner has been activated!");
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