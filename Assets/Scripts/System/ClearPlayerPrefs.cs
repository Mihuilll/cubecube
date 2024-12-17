using UnityEngine;
using UnityEngine.UI;

public class ClearPlayerPrefs : MonoBehaviour
{
    public Button clearButton;  // ������, �� ������� ����� ������ ��� �������� ������

    private void Start()
    {
        // ������������� ���������� ������� �� ������
        if (clearButton != null)
        {
            clearButton.onClick.AddListener(ClearAllPlayerPrefs);
        }
    }

    // ������� ��� �������� ���� ���������� ������
    private void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();  // ������� ��� ���������� ������
        PlayerPrefs.Save();  // ��������� ���������
        Debug.Log("��� ������ PlayerPrefs ���� �������!");
    }
}
