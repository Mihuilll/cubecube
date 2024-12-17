using UnityEngine;
using UnityEngine.UI;

public class SkinManager : SoundPolomorf
{
    public Button[] skinButtons; // ������ ������ ������
    private int selectedSkin = 0; // ��������� ����

    [SerializeField] private GameObject skisns;
    [SerializeField] private GameObject menuPanel;

    // ������ ������ ��� ������
    public Color[] skinColors = { Color.white, Color.red, Color.green, Color.blue };

    private void UpdateSkins()
    {
        // ��������� ��������� ����
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);

        UpdateSkinButtons();
        ApplySkin(selectedSkin);

        for (int i = 0; i < skinButtons.Length; i++)
        {
            int index = i;
            skinButtons[i].onClick.AddListener(() => SelectSkin(index));
        }
    }

    void UpdateSkinButtons()
    {
        // ��������� ���������������� �����
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt($"Skin_Biome_{i}_Unlocked", i == 0 ? 1 : 0) == 1;
            skinButtons[i].interactable = unlocked;

            skinButtons[i].GetComponentInChildren<Text>().text = unlocked ? $"���� {i + 1}" : "������������";
        }
    }

    public void SelectSkin(int index)
    {
        selectedSkin = index;
        PlayerPrefs.SetInt("SelectedSkin", selectedSkin);

        // ��������� ���� ���������� �����
        PlayerPrefs.SetFloat("SkinColor_R", skinColors[index].r);
        PlayerPrefs.SetFloat("SkinColor_G", skinColors[index].g);
        PlayerPrefs.SetFloat("SkinColor_B", skinColors[index].b);

        PlayerPrefs.Save();

        ApplySkin(index);
        Debug.Log($"���� {index} ������!");
    }

    void ApplySkin(int index)
    {
        Debug.Log($"������� ���� {index}");
    }

    public void P_Open_Skisns()
    {
        AudioManager.instance.PlayUISound(clip);

        menuPanel.SetActive(false);
        skisns.SetActive(true);
        UpdateSkins();
    }

    public void P_Exit_Skisns()
    {
        AudioManager.instance.PlayUISound(clip);

        skisns.SetActive(false);
        menuPanel.SetActive(true);
    }
}
