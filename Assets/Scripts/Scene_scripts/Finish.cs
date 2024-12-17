using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private int indexScene; // Следующая сцена (уровень)
    [SerializeField]
    private int currentLevel; // Текущий уровень
    [SerializeField]
    private int biomeIndex; // Номер биома (0 - первый, 1 - второй и т.д.)
    [SerializeField]
    private int totalLevelsInBiome; // Общее количество уровней в биоме

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Сохраняем текущий уровень как завершённый
            PlayerPrefs.SetInt($"Level_{biomeIndex}_{currentLevel}_Completed", 1);
            PlayerPrefs.Save();

            // Проверяем, завершены ли все уровни в биоме
            if (IsBiomeCompleted())
            {
                UnlockBiomeSkin();
            }

            // Переход на следующую сцену
            SceneManager.LoadScene(indexScene);
        }
    }

    private bool IsBiomeCompleted()
    {
        // Проверяем все уровни биома
        for (int i = 0; i < totalLevelsInBiome; i++)
        {
            if (PlayerPrefs.GetInt($"Level_{biomeIndex}_{i}_Completed", 0) == 0)
            {
                return false; // Найден незавершённый уровень
            }
        }
        return true; // Все уровни биома завершены
    }

    private void UnlockBiomeSkin()
    {
        PlayerPrefs.SetInt($"Skin_Biome_{biomeIndex}_Unlocked", 1);
        PlayerPrefs.Save();
        Debug.Log($"Скин для биома {biomeIndex} разблокирован!");
    }
}
