using UnityEngine;

public class TeleportWithEffect : Splashes
{
    [SerializeField]
    private GameObject[] checkpoints;
    [SerializeField]
    private int currentCheckpointIndex = 0;


    // Метод для получения позиции текущей контрольной точки
    public Vector3 GetCurrentCheckpointPosition()
    {
        if (checkpoints.Length == 0)
        {
            return Vector3.zero; // Возвращаем Vector3.zero, если массив пуст
        }

        return checkpoints[currentCheckpointIndex].transform.position;
    }
}
