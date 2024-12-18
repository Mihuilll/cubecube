using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class silectlvl : SoundPolomorf
{
    public GameObject biomsPanel;
    public GameObject LvlPanel;

    public Button[] levelButtons; // Кнопки для выбора уровней
    public string[] nameScene;    // Названия сцен для уровней
    public Image[] levelButtonImages; // Массив изображений кнопок (для изменения прозрачности)

    private void Start()
    {
        UpdateLevelButtons();

        // Добавляем слушателей на кнопки
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i; // Создаем копию переменной для замыкания
            levelButtons[i].onClick.AddListener(() => P_Open_Lvl(index));
        }
    }

    void UpdateLevelButtons()
    {
        int biomeIndex = ProgressManager.Instance.currentBiomeIndex; // Получаем текущий биом
        int totalLevelsInBiome = ProgressManager.Instance.GetTotalLevelsInBiome(biomeIndex); // Получаем количество уровней для текущего биома

        // Активируем кнопки уровней на основе прогресса
        for (int i = 0; i < totalLevelsInBiome; i++) // Используем totalLevelsInBiome вместо levelButtons.Length
        {
            bool isLevelCompleted = ProgressManager.Instance.IsLevelCompleted(biomeIndex, i); // Проверка, завершён ли уровень

            // Если это первый уровень, он всегда доступен
            if (i == 0)
            {
                levelButtons[i].interactable = true;
                SetButtonTransparency(i, 1f); // Убираем полупрозрачность для первого уровня
            }
            else
            {
                // Проверяем, был ли завершён предыдущий уровень
                bool isLevelUnlocked = ProgressManager.Instance.IsLevelCompleted(biomeIndex, i - 1);
                levelButtons[i].interactable = isLevelUnlocked;
                SetButtonTransparency(i, isLevelUnlocked ? 1f : 0.5f); // Полупрозрачность для заблокированных уровней
            }

            // Для последнего уровня — всегда открыт, если все предыдущие пройдены
            if (i == totalLevelsInBiome - 1)
            {
                levelButtons[i].interactable = true;
                SetButtonTransparency(i, 1f); // Убираем полупрозрачность для последнего уровня
            }
        }
    }

    // Функция для изменения прозрачности кнопки
    private void SetButtonTransparency(int index, float alpha)
    {
        if (levelButtonImages != null && levelButtonImages.Length > index)
        {
            Color buttonColor = levelButtonImages[index].color;
            buttonColor.a = alpha; // Изменяем альфа-канал (прозрачность)
            levelButtonImages[index].color = buttonColor;
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
