using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Note note;
    public AudioSource footstepsSound;
    private bool isWaiting = false;

    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !isWaiting && !Note.isNoteOpen && !PauseMenu.isPaused)
        {
            if (Input.GetKey(KeyCode.Space) && !isWaiting)
            {
                StartCoroutine(WaitForFootsteps());
            }
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }

    IEnumerator WaitForFootsteps()
    {
        isWaiting = true; 

        yield return new WaitForSeconds(1f);

        footstepsSound.enabled = false;

        isWaiting = false; 
    }
}