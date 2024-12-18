using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    // Пример определения currentBiomeIndex
    public int currentBiomeIndex { get; private set; }
    // Массив для отслеживания завершенности уровней в каждом биоме
    private int[][] levelCompletionData;  // Индекс [биом][уровень] = 0 или 1

    // Массив для хранения количества уровней в каждом биоме
    private int[] totalLevelsInBiomes = { 3, 4 };  // Пример: 3 уровня в первом биоме, 4 во втором

    void Awake()
    {
        // Инициализация синглтона
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Инициализация данных о завершенности уровней
        levelCompletionData = new int[totalLevelsInBiomes.Length][];
        for (int i = 0; i < totalLevelsInBiomes.Length; i++)
        {
            levelCompletionData[i] = new int[totalLevelsInBiomes[i]];
            // Заполняем уровни как не завершенные по умолчанию
            for (int j = 0; j < totalLevelsInBiomes[i]; j++)
            {
                levelCompletionData[i][j] = PlayerPrefs.GetInt($"Level_{i}_{j}_Completed", 0);  // 0 - не завершен
            }
        }
    }

    // Метод для проверки, завершены ли все уровни в биоме
    public bool IsBiomeCompleted(int biomeIndex)
    {
        if (biomeIndex >= 0 && biomeIndex < levelCompletionData.Length)
        {
            for (int i = 0; i < levelCompletionData[biomeIndex].Length; i++)
            {
                if (levelCompletionData[biomeIndex][i] == 0) // Если хотя бы один уровень не завершён
                {
                    return false;
                }
            }
            return true; // Все уровни завершены
        }
        return false;
    }

    // Метод для сохранения завершения уровня
    public void SetLevelCompleted(int biomeIndex, int levelIndex, bool completed)
    {
        if (biomeIndex >= 0 && biomeIndex < levelCompletionData.Length &&
            levelIndex >= 0 && levelIndex < levelCompletionData[biomeIndex].Length)
        {
            levelCompletionData[biomeIndex][levelIndex] = completed ? 1 : 0; // Сохраняем статус завершения
            PlayerPrefs.SetInt($"Level_{biomeIndex}_{levelIndex}_Completed", completed ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    // Метод для разблокировки скина для биома
    public void UnlockBiomeSkin(int biomeIndex)
    {
        PlayerPrefs.SetInt($"Skin_Biome_{biomeIndex}_Unlocked", 1);
        PlayerPrefs.Save();
        Debug.Log($"Скин для биома {biomeIndex} разблокирован!");
    }

    public bool IsLevelCompleted(int biomeIndex, int levelIndex)
    {
        // Возвращаем, завершён ли уровень в биоме
        return PlayerPrefs.GetInt($"Level_{biomeIndex}_{levelIndex}_Completed", 0) == 1;
    }

    public int GetTotalLevelsInBiome(int biomeIndex)
    {
        // Возвращаем общее количество уровней для текущего биома
        return totalLevelsInBiomes[biomeIndex];
    }

}
