using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex; // ��������� ����� (�������)
    [SerializeField] private int currentLevelIndex; // ������� �������
    [SerializeField] private int biomeIndex; // ����� �����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ��������� ������� �������
            ProgressManager.Instance.SetLevelCompleted(biomeIndex, currentLevelIndex, true);

            // ���������, ��������� �� ��� ������ � �����
            if (ProgressManager.Instance.IsBiomeCompleted(biomeIndex))
            {
                Debug.Log("��� ������ ����� ���������!");
                ProgressManager.Instance.UnlockBiomeSkin(biomeIndex + 1); // ������������ ��������� ����
            }

            // ������� �� ��������� �����
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}