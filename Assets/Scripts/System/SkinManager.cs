using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public Button[] skinButtons; // Кнопки выбора скинов
    private int selectedSkin = 0; // Выбранный скин

    [SerializeField] private GameObject skisns;
    [SerializeField] private GameObject menuPanel;

    // Массив цветов для скинов
    public Color[] skinColors = { Color.white, Color.red, Color.green, Color.blue };

    private void Start()
    {
        UpdateSkins();
    }

    private void UpdateSkins()
    {
        // Загружаем выбранный скин
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);

        UpdateSkinButtons();

        for (int i = 0; i < skinButtons.Length; i++)
        {
            int index = i;
            skinButtons[i].onClick.AddListener(() => SelectSkin(index));
        }
    }

    void UpdateSkinButtons()
    {
        // Проверяем разблокированные скины
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt($"Skin_Biome_{i}_Unlocked", i == 0 ? 1 : 0) == 1;
            skinButtons[i].interactable = unlocked;
            Debug.Log(unlocked);
            // Устанавливаем текст кнопки в зависимости от доступности скина
            Text buttonText = skinButtons[i].GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = unlocked ? $"Скин {i + 1}" : "Заблокирован";
            }
        }
    }

    public void SelectSkin(int index)
    {
        selectedSkin = index;
        PlayerPrefs.SetInt("SelectedSkin", selectedSkin);

        // Сохраняем цвет выбранного скина
        PlayerPrefs.SetFloat("SkinColor_R", skinColors[index].r);
        PlayerPrefs.SetFloat("SkinColor_G", skinColors[index].g);
        PlayerPrefs.SetFloat("SkinColor_B", skinColors[index].b);

        PlayerPrefs.Save();

        Debug.Log($"Скин {index} выбран!");
    }


    public void P_Open_Skisns()
    {
        menuPanel.SetActive(false);
        skisns.SetActive(true);
        UpdateSkins();
    }

    public void P_Exit_Skisns()
    {
        skisns.SetActive(false);
        menuPanel.SetActive(true);
    }
}