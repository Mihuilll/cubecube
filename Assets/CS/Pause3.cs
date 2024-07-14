using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause3 : MonoBehaviour
{
    private bool paused = false;
    public GameObject panel;
    public GameObject panel1;
    public float time = 10;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        panel1.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
                panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                paused = false;
                panel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.
            GetActiveScene().buildIndex - 3);
        }
    }
}
