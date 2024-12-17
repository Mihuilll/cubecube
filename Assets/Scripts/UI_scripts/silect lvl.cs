using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class silectlvl : SoundPolomorf
{
    public GameObject biomsPanel;
    public GameObject LvlPanel;
    public Button[] buttons;
    public string[] nameScene;

    private void Start()
    {
        // Назначаем обработчик события для каждой кнопки
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Создаем копию переменной для замыкания
            buttons[i].onClick.AddListener(() => P_Open_Lvl(index));
        }
    }
    public void P_Open_Lvl(int index)
    {
        AudioManager.instance.PlayUISound(clip);

        biomsPanel.SetActive(false);
        SceneManager.LoadScene(nameScene[index]);
    }
    public void P_Close_SelectLvl()
    {
        AudioManager.instance.PlayUISound(clip);

        biomsPanel.SetActive(true);
        LvlPanel.SetActive(false);
    }
}
