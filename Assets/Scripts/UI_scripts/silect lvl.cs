using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class silectlvl : SoundPolomorf
{
    public GameObject biomsPanel;
    public GameObject LvlPanel;

    public Button[] levelButtons; // Кнопки для выбора уровней
    public string[] nameScene;    // Названия сцен для уровней

    private void Start()
    {
        UpdateLevelButtons();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i; // Создаем копию переменной для замыкания
            levelButtons[i].onClick.AddListener(() => P_Open_Lvl(index));
        }
    }

    void UpdateLevelButtons()
    {
        // Активируем кнопки уровней на основе прогресса
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == 0)
            {
                // Первый уровень всегда открыт
                levelButtons[i].interactable = true;
            }
            else
            {
                // Проверяем, пройден ли предыдущий уровень
                int previousLevel = PlayerPrefs.GetInt($"Level_{i}_Completed", 0);
                levelButtons[i].interactable = previousLevel == 1;
            }
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
