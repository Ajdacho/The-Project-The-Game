using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public string noteText;
    public GameObject noteUI;
    public TMP_Text noteContent;
    private bool isPlayerNearby = false;

    void Start()
    {
        noteUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenNote();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    public void OpenNote()
    {
        noteUI.SetActive(true);
        noteContent.text = noteText;
        Time.timeScale = 0f;
    }


    public void CloseNote()
    {
        noteUI.SetActive(false);  // Ukrywa notatkê
        Time.timeScale = 1f;  // Wznawia grê
        Debug.Log("Note closed.");
    }

}