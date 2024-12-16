using UnityEngine;

public class MainMenu : SoundPolomorf
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject selectBioms;
    [SerializeField] private GameObject option;
    
    public void P_Open_SelectBioms()
    {
        // ��������������� ����� UI ����� ���������� ����
        AudioManager.instance.PlayUISound(clip);

        menuPanel.SetActive(false);
        selectBioms.SetActive(true);
    }

    public void P_Open_Options()
    {
        // ��������������� ����� UI ����� ���������� ����
        AudioManager.instance.PlayUISound(clip);

        menuPanel.SetActive(false);
        option.SetActive(true);
    }
}
