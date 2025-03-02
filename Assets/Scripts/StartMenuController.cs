using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 0f;
        AudioListener.volume = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnStartClick()
    {
        Time.timeScale = 1.0f;
        AudioListener.volume = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
