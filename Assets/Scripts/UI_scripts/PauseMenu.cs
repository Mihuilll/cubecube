using UnityEngine;
using UnityEngine.SceneManagement;
// the script needs a test
public class PauseMenu : SoundPolomorf
{
    [SerializeField] private string sceneMenu;
    [SerializeField] private GameObject pauseMenu;
    public void P_Pause()
    {
        AudioManager.instance.PlayUISound(clip);

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void P_Resume()
    {
        AudioManager.instance.PlayUISound(clip);

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        AudioManager.instance.PlayUISound(clip);

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneMenu);
    }
}