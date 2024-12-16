using UnityEngine;
using UnityEngine.SceneManagement;
// the script needs a test
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string sceneMenu;
    [SerializeField] private GameObject pauseMenu;
    public void P_Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void P_Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneMenu);
    }
}