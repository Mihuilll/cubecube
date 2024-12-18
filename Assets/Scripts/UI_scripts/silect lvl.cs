using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class silectlvl : MonoBehaviour
{
    public int biomeIndex; // Уникальный индекс биома для этого скрипта
    public GameObject biomsPanel; // Панель выбора биома
    public GameObject[] levelPanels; // Панели выбора уровней для каждого биома
    public Button[] levelButtons; // Кнопки для выбора уровней
    public string[] nameScene; // Названия сцен для уровней
    public Image[] levelButtonImages; // Изображения кнопок уровней

    private void Start()
    {
        UpdateLevelButtons();
        AssignButtonListeners();
    }

    private void AssignButtonListeners()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;
            levelButtons[i].onClick.AddListener(() => P_Open_Lvl(index));
        }
    }

    private void UpdateLevelButtons()
    {
        int totalLevels = ProgressManager.Instance.GetTotalLevelsInBiome(biomeIndex);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < totalLevels)
            {
                bool isCompleted = ProgressManager.Instance.IsLevelCompleted(biomeIndex, i);
                bool isUnlocked = i == 0 || ProgressManager.Instance.IsLevelCompleted(biomeIndex, i - 1);

                levelButtons[i].interactable = isUnlocked;
                SetButtonTransparency(i, isUnlocked ? 1f : 0.5f);
            }
            else
            {
                levelButtons[i].interactable = false;
                SetButtonTransparency(i, 0.5f);
            }
        }
    }

    private void SetButtonTransparency(int index, float alpha)
    {
        if (levelButtonImages != null && index < levelButtonImages.Length)
        {
            Color color = levelButtonImages[index].color;
            color.a = alpha;
            levelButtonImages[index].color = color;
        }
    }

    public void P_Open_Lvl(int index)
    {
        biomsPanel.SetActive(false);
        SceneManager.LoadScene(nameScene[index]);
    }

    public void P_Close_SelectLvl()
    {
        biomsPanel.SetActive(true);
        foreach (var panel in levelPanels)
        {
            panel.SetActive(false);
        }
    }

    public void P_Open_BiomePanel()
    {
        biomsPanel.SetActive(false);
        foreach (var panel in levelPanels)
        {
            panel.SetActive(false);
        }
        levelPanels[biomeIndex].SetActive(true);
        ProgressManager.Instance.SetCurrentBiomeIndex(biomeIndex);
        UpdateLevelButtons();
    }

    public void P_Exit_BiomePanel()
    {
        levelPanels[biomeIndex].SetActive(false);
        biomsPanel.SetActive(true);
    }
}
