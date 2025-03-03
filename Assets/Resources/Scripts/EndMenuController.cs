using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 0f;
        AudioListener.volume = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
