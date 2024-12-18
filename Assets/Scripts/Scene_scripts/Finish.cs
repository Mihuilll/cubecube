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
            // Завершаем текущий уровень в ProgressManager
            ProgressManager.Instance.SetLevelCompleted(biomeIndex, currentLevel, true);

            Debug.Log($"Уровень {currentLevel} в биоме {biomeIndex} завершён.");

            // Проверяем, завершены ли все уровни в биоме
            if (ProgressManager.Instance.IsBiomeCompleted(biomeIndex))
            {
                Debug.Log("Все уровни биома завершены!");
                ProgressManager.Instance.UnlockBiomeSkin(biomeIndex);
            }

            // Переход на следующую сцену
            SceneManager.LoadScene(indexScene);
        }
    }
}
