using UnityEngine;
using UnityEngine.UI;
// for correct operation (button index == panelLvl index)
// дл коректной работы (индекс button == индексу panelLvl)
public class SelectBioms : MonoBehaviour
{
    public GameObject biomsPanel;
    public GameObject menuPanel;
    public Button[] buttons;
    public GameObject[] panelLvl;

    private void Start()
    {
        // Назначаем обработчик события для каждой кнопки
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Создаем копию переменной для замыкания
            buttons[i].onClick.AddListener(() => P_Open_SelectLvl(index));
        }
    }
    public void P_Open_SelectLvl(int index)
    {
        biomsPanel.SetActive(false);
        panelLvl[0].SetActive(true);
    }
    public void P_Close_SelectBioms()
    {
        biomsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
