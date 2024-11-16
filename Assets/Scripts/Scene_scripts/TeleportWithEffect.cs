using UnityEngine;

public class TeleportWithEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] checkpoints; // Массив контрольных точек

    [SerializeField]
    private GameObject particlePrefab; // Префаб кубика для эффекта

    [SerializeField]
    private float explosionForce = 5f; // Сила взрыва

    [SerializeField]
    private int particleCount = 10; // Количество кубиков

    private int currentCheckpointIndex = 0; // Индекс текущей контрольной точки

    // Метод для получения позиции текущей контрольной точки
    public Vector3 GetCurrentCheckpointPosition()
    {
        if (checkpoints.Length == 0)
        {
            Debug.LogWarning("Checkpoints array is empty!");
            return Vector3.zero; // Возвращаем Vector3.zero, если массив пуст
        }

        return checkpoints[currentCheckpointIndex].transform.position;
    }

    // Метод для создания эффекта "взрыва"
    public void CreateExplosion(Vector3 position)
    {
        for (int i = 0; i < particleCount; i++)
        {
            // Создаем кубик
            GameObject particle = Instantiate(particlePrefab, position, Random.rotation);

            // Добавляем компонент Rigidbody для физики
            Rigidbody rb = particle.AddComponent<Rigidbody>();

            // Направляем кубик в случайную сторону
            Vector3 randomDirection = Random.onUnitSphere;

            // Применяем силу взрыва
            rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);

            // Уничтожаем кубик через 3 секунды
            Destroy(particle, 3f);
        }
    }
}
