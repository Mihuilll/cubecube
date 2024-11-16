using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Переменная для управления скоростью вращения
    [SerializeField]
    private float sf = 10f;

    // Направление вращения: 1 - вправо, -1 - влево
    private int rotationDirection;

    // Метод Start вызывается при запуске сцены
    void Start()
    {
        // Случайно выбираем направление вращения
        rotationDirection = Random.value > 0.5f ? 1 : -1;
    }

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Вращаем объект вокруг оси Y
        transform.Rotate(0, rotationDirection * sf * Time.deltaTime, 0);
    }
}
