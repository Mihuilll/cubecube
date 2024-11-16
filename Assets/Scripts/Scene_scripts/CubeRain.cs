using UnityEngine;

public class CubeRain : MonoBehaviour
{
    // Префаб куба (его нужно создать заранее)
    public GameObject cubePrefab;

    // Позиция спауна (центр области)
    public Vector3 spawnAreaCenter;

    // Размеры области спауна
    public Vector3 spawnAreaSize;

    // Время между спаунами
    public float spawnInterval = 0.5f;

    // Скорость разрушения упавших кубов
    public float destroyDelay = 5f;

    private void Start()
    {
        // Начинаем цикл спауна кубов
        InvokeRepeating(nameof(SpawnCube), 0f, spawnInterval);
    }

    private void SpawnCube()
    {
        // Случайная позиция внутри области спауна
        Vector3 spawnPosition = spawnAreaCenter + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        // Создаем куб
        GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        // Уничтожаем куб через destroyDelay секунд
        Destroy(cube, destroyDelay);
    }

    private void OnDrawGizmos()
    {
        // Рисуем область спауна в редакторе
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
