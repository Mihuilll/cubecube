using UnityEngine;

public class MainMenu : MonoBehaviour
{ 
    [SerializeField] private GameObject menuPanel; 
    [SerializeField] private GameObject selectBioms;
    [SerializeField] private GameObject option;

    public void P_Open_SelectBioms()
    {
        menuPanel.SetActive(false);
        selectBioms.SetActive(true);        
    }
    public void P_Open_Options()
    {
        menuPanel.SetActive(false);
        option.SetActive(true);
    }
}