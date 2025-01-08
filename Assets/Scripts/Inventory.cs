using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log($"Added {item.name} to inventory.");
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
