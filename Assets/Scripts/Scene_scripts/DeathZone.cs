using UnityEngine;

public class DeathZoneChecker : MonoBehaviour
{
    // Ссылка на глобальный контроллер для работы с чекпоинтами и эффектами
    public TeleportWithEffect particleController;

    // Границы сцены
    public Vector3 minBounds; // Минимальные координаты (x, y, z)
    public Vector3 maxBounds; // Максимальные координаты (x, y, z)

    // Цвет границ
    public Color boundsColor = Color.red;

    // Объект игрока
    public Transform player;

    // Флаг, чтобы предотвратить повторное срабатывание
    private bool isPlayerOutOfBounds = false;

    private void Update()
    {
        if (IsOutOfBounds(player.position))
        {
            if (!isPlayerOutOfBounds)
            {
                // Обрабатываем выход игрока за пределы
                HandlePlayerOutOfBounds();
                isPlayerOutOfBounds = true; // Устанавливаем флаг, чтобы не повторять
            }
        }
        else
        {
            isPlayerOutOfBounds = false; // Сбрасываем флаг, если игрок вернулся в границы
        }
    }

    // Проверяем, находится ли игрок за пределами границ
    private bool IsOutOfBounds(Vector3 position)
    {
        return position.x < minBounds.x || position.x > maxBounds.x ||
               position.y < minBounds.y || position.y > maxBounds.y ||
               position.z < minBounds.z || position.z > maxBounds.z;
    }

    // Обработка выхода игрока за границы
    private void HandlePlayerOutOfBounds()
    {
        // Создаем эффект на месте падения
        particleController.CreateExplosion(player.position);

        // Телепортируем игрока к текущей контрольной точке
        TeleportPlayer();

        // Создаем эффект на новой позиции
        particleController.CreateExplosion(particleController.GetCurrentCheckpointPosition());
    }

    private void TeleportPlayer()
    {
        // Получаем позицию текущего чекпоинта
        Vector3 checkpointPosition = particleController.GetCurrentCheckpointPosition();

        // Перемещаем игрока
        player.position = checkpointPosition;

        Debug.Log($"Player teleported to checkpoint: {checkpointPosition}");
    }

    // Рисуем границы в редакторе (Scene View)
    private void OnDrawGizmos()
    {
        // Настраиваем цвет каркасного куба
        Gizmos.color = boundsColor;

        // Рассчитываем размеры границы
        Vector3 size = maxBounds - minBounds;
        Vector3 center = minBounds + size / 2;

        // Рисуем каркасный куб в редакторе
        Gizmos.DrawWireCube(center, size);
    }
}
