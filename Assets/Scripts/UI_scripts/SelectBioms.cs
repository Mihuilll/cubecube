using UnityEngine;
using UnityEngine.UI;

public class SelectBioms : SoundPolomorf
{
    public GameObject biomsPanel;
    public GameObject menuPanel;
    public Button[] buttons;
    public GameObject[] panelLvl;

 
    public void P_Close_SelectBioms()
    {
        AudioManager.instance.PlayUISound(clip);

        biomsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}