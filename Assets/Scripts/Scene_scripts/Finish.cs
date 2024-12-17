using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private int indexScene; // ��������� ����� (�������)
    [SerializeField]
    private int currentLevel; // ������� �������
    [SerializeField]
    private int biomeIndex; // ����� ����� (0 - ������, 1 - ������ � �.�.)
    [SerializeField]
    private int totalLevelsInBiome; // ����� ���������� ������� � �����

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ��������� ������� ������� ��� �����������
            PlayerPrefs.SetInt($"Level_{biomeIndex}_{currentLevel}_Completed", 1);
            PlayerPrefs.Save();

            // ���������, ��������� �� ��� ������ � �����
            if (IsBiomeCompleted())
            {
                UnlockBiomeSkin();
            }

            // ������� �� ��������� �����
            SceneManager.LoadScene(indexScene);
        }
    }

    private bool IsBiomeCompleted()
    {
        // ��������� ��� ������ �����
        for (int i = 0; i < totalLevelsInBiome; i++)
        {
            if (PlayerPrefs.GetInt($"Level_{biomeIndex}_{i}_Completed", 0) == 0)
            {
                return false; // ������ ������������� �������
            }
        }
        return true; // ��� ������ ����� ���������
    }

    private void UnlockBiomeSkin()
    {
        PlayerPrefs.SetInt($"Skin_Biome_{biomeIndex}_Unlocked", 1);
        PlayerPrefs.Save();
        Debug.Log($"���� ��� ����� {biomeIndex} �������������!");
    }
}
