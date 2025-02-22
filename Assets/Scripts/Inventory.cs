using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject scanner;
    private void Start()
    {
        scanner = transform.Find("Scanner")?.gameObject;
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