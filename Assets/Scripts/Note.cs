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
    PauseMenu pauseMenu;

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
        Debug.Log("megustalamaniana");
        Cursor.lockState = CursorLockMode.Confined;
        noteUI.SetActive(true);
        noteContent.text = noteText;
        Time.timeScale = 0f;
    }


    public void CloseNote()
    {
        Cursor.lockState = CursorLockMode.Locked;
        noteUI.SetActive(false);  // Ukrywa notatke
        Time.timeScale = 1f;  // Wznawia gre
        Debug.Log("LETS GOOOOOOOOOOOO");
    }

}