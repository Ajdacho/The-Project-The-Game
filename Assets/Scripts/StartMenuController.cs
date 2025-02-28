using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnStartClick()
    {
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
