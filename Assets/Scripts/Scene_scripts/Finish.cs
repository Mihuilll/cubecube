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
            // ��������� ������� ������� � ProgressManager
            ProgressManager.Instance.SetLevelCompleted(biomeIndex, currentLevel, true);

            Debug.Log($"������� {currentLevel} � ����� {biomeIndex} ��������.");

            // ���������, ��������� �� ��� ������ � �����
            if (ProgressManager.Instance.IsBiomeCompleted(biomeIndex))
            {
                Debug.Log("��� ������ ����� ���������!");
                ProgressManager.Instance.UnlockBiomeSkin(biomeIndex);
            }

            // ������� �� ��������� �����
            SceneManager.LoadScene(indexScene);
        }
    }
}
