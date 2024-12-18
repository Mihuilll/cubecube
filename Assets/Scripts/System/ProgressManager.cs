using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    private int[][] levelCompletionData;
    private int[] totalLevelsInBiomes = { 4, 4,4 }; // Пример данных
    public int currentBiomeIndex { get; private  set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeProgressData();
    }

    private void InitializeProgressData()
    {
        levelCompletionData = new int[totalLevelsInBiomes.Length][];

        for (int i = 0; i < totalLevelsInBiomes.Length; i++)
        {
            levelCompletionData[i] = new int[totalLevelsInBiomes[i]];
            for (int j = 0; j < totalLevelsInBiomes[i]; j++)
            {
                levelCompletionData[i][j] = PlayerPrefs.GetInt($"Level_{i}_{j}_Completed", 0);
            }
        }

        currentBiomeIndex = PlayerPrefs.GetInt("CurrentBiomeIndex", 0);
    }

    public void SetLevelCompleted(int biomeIndex, int levelIndex, bool completed)
    {
        if (biomeIndex < 0 || biomeIndex >= levelCompletionData.Length ||
            levelIndex < 0 || levelIndex >= levelCompletionData[biomeIndex].Length)
            return;

        levelCompletionData[biomeIndex][levelIndex] = completed ? 1 : 0;
        PlayerPrefs.SetInt($"Level_{biomeIndex}_{levelIndex}_Completed", completed ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsLevelCompleted(int biomeIndex, int levelIndex)
    {
        return levelCompletionData[biomeIndex][levelIndex] == 1;
    }

    public bool IsBiomeCompleted(int biomeIndex)
    {
        foreach (int level in levelCompletionData[biomeIndex])
        {
            Debug.Log($"Biome {biomeIndex} Level State: {level}");
            if (level == 0) return false;
        }
        return true;
    }


    public void UnlockBiomeSkin(int biomeIndex)
    {
        Debug.Log($"Unlocking skin for biome {biomeIndex}");
        PlayerPrefs.SetInt($"Skin_Biome_{biomeIndex}_Unlocked", 1);
        PlayerPrefs.Save();
    }


    public int GetTotalLevelsInBiome(int biomeIndex)
    {
        return totalLevelsInBiomes[biomeIndex];
    }

    public void SetCurrentBiomeIndex(int biomeIndex)
    {
        currentBiomeIndex = biomeIndex;
        PlayerPrefs.SetInt("CurrentBiomeIndex", biomeIndex);
        PlayerPrefs.Save();
    }
}