using UnityEngine;

public class MainMenu : SoundPolomorf
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject selectBioms;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject skisns;
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

    public void P_Exit_Option()
    {
        // ��������������� ����� UI ����� ���������� ����
        AudioManager.instance.PlayUISound(clip);

        option.SetActive(false);
        menuPanel.SetActive(true);
    }



}
