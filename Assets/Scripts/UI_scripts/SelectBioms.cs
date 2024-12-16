using UnityEngine;
using UnityEngine.UI;

public class SelectBioms : SoundPolomorf
{
    public GameObject biomsPanel;
    public GameObject menuPanel;
    public Button[] buttons;
    public GameObject[] panelLvl;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; 
            buttons[i].onClick.AddListener(() => P_Open_SelectLvl(index));
        }
    }
    public void P_Open_SelectLvl(int index)
    {
        AudioManager.instance.PlayUISound(clip);

        biomsPanel.SetActive(false);
        panelLvl[0].SetActive(true);
    }
    public void P_Close_SelectBioms()
    {
        AudioManager.instance.PlayUISound(clip);

        biomsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}