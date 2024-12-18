using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex; // Следующая сцена (уровень)
    [SerializeField] private int currentLevelIndex; // Текущий уровень
    [SerializeField] private int biomeIndex; // Номер биома

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Завершаем текущий уровень
            ProgressManager.Instance.SetLevelCompleted(biomeIndex, currentLevelIndex, true);

            // Проверяем, завершены ли все уровни в биоме
            if (ProgressManager.Instance.IsBiomeCompleted(biomeIndex))
            {
                Debug.Log("Все уровни биома завершены!");
                ProgressManager.Instance.UnlockBiomeSkin(biomeIndex + 1); // Разблокируем следующий биом
            }

            // Переход на следующую сцену
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}