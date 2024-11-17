using UnityEngine;

public class DeathZoneChecker : MonoBehaviour
{
    // Минимальная высота, ниже которой игрок считается "выпавшим"
    public float minHeight = -10f;

    // Ссылка на объект игрока
    public Transform player;

    // Ссылка на контроллер чекпоинтов
    public TeleportWithEffect particleController;

    private void Update()
    {
        // Проверяем, если игрок ниже минимальной высоты
        if (player.position.y < minHeight)
        {
            HandlePlayerOutOfBounds();
        }
    }

    private void HandlePlayerOutOfBounds()
    {
        // Создаём эффект на месте падения
        particleController.CreateExplosion(player.position);

        // Телепортируем игрока на текущий чекпоинт
        TeleportPlayer();

        // Создаём эффект на новой позиции
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
}
