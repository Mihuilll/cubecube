using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    // ������ ����������� currentBiomeIndex
    public int currentBiomeIndex { get; private set; }
    // ������ ��� ������������ ������������� ������� � ������ �����
    private int[][] levelCompletionData;  // ������ [����][�������] = 0 ��� 1

    // ������ ��� �������� ���������� ������� � ������ �����
    private int[] totalLevelsInBiomes = { 3, 4 };  // ������: 3 ������ � ������ �����, 4 �� ������

    void Awake()
    {
        // ������������� ���������
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // ������������� ������ � ������������� �������
        levelCompletionData = new int[totalLevelsInBiomes.Length][];
        for (int i = 0; i < totalLevelsInBiomes.Length; i++)
        {
            levelCompletionData[i] = new int[totalLevelsInBiomes[i]];
            // ��������� ������ ��� �� ����������� �� ���������
            for (int j = 0; j < totalLevelsInBiomes[i]; j++)
            {
                levelCompletionData[i][j] = PlayerPrefs.GetInt($"Level_{i}_{j}_Completed", 0);  // 0 - �� ��������
            }
        }
    }

    // ����� ��� ��������, ��������� �� ��� ������ � �����
    public bool IsBiomeCompleted(int biomeIndex)
    {
        if (biomeIndex >= 0 && biomeIndex < levelCompletionData.Length)
        {
            for (int i = 0; i < levelCompletionData[biomeIndex].Length; i++)
            {
                if (levelCompletionData[biomeIndex][i] == 0) // ���� ���� �� ���� ������� �� ��������
                {
                    return false;
                }
            }
            return true; // ��� ������ ���������
        }
        return false;
    }

    // ����� ��� ���������� ���������� ������
    public void SetLevelCompleted(int biomeIndex, int levelIndex, bool completed)
    {
        if (biomeIndex >= 0 && biomeIndex < levelCompletionData.Length &&
            levelIndex >= 0 && levelIndex < levelCompletionData[biomeIndex].Length)
        {
            levelCompletionData[biomeIndex][levelIndex] = completed ? 1 : 0; // ��������� ������ ����������
            PlayerPrefs.SetInt($"Level_{biomeIndex}_{levelIndex}_Completed", completed ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    // ����� ��� ������������� ����� ��� �����
    public void UnlockBiomeSkin(int biomeIndex)
    {
        PlayerPrefs.SetInt($"Skin_Biome_{biomeIndex}_Unlocked", 1);
        PlayerPrefs.Save();
        Debug.Log($"���� ��� ����� {biomeIndex} �������������!");
    }

    public bool IsLevelCompleted(int biomeIndex, int levelIndex)
    {
        // ����������, �������� �� ������� � �����
        return PlayerPrefs.GetInt($"Level_{biomeIndex}_{levelIndex}_Completed", 0) == 1;
    }

    public int GetTotalLevelsInBiome(int biomeIndex)
    {
        // ���������� ����� ���������� ������� ��� �������� �����
        return totalLevelsInBiomes[biomeIndex];
    }

}
