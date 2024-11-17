using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Подключаем пространство имен UI

public class CameraController : MonoBehaviour
{
    // Переменные для вращения камеры
    public float mouseSensitivity = 0.1f; // Скорость изменения угла
    public float rotationSpeed = 5f; // Скорость вращения
    public float inertiaDamping = 0.95f; // Коэффициент затухания инерции
    public FixedJoystick joystick; // Ссылка на фиксированный джойстик из Joystick Pack

    private float currentAngle = 0f; // Текущий угол
    private float rotationVelocity = 0f; // Скорость вращения
    private GraphicRaycaster raycaster; // Добавляем для работы с UI элементами
    private EventSystem eventSystem;

    // Переменные для изменения высоты камеры
    public Transform player;          // Ссылка на игрока
    public float minHeight = 5f;      // Минимальная высота камеры
    public float maxHeight = 15f;     // Максимальная высота камеры
    public float heightSmoothSpeed = 2f; // Скорость плавного движения камеры по оси Y
    public float cameraWindowHeight = 5f; // Высота окна камеры относительно игрока

    private Vector3 currentCameraPosition;
    private Vector3 targetCameraPosition;

    void Start()
    {
        // Получаем ссылки на GraphicRaycaster и EventSystem
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();

        // Проверка наличия необходимых компонентов
        if (raycaster == null)
        {
            Debug.LogError("GraphicRaycaster not found in the scene. Please add a Canvas with a GraphicRaycaster.");
        }

        if (eventSystem == null)
        {
            Debug.LogError("EventSystem not found in the scene. Please add an EventSystem.");
        }

        if (joystick == null)
        {
            Debug.LogError("Fixed Joystick not found. Please assign it in the inspector.");
        }

        // Инициализируем начальное положение камеры
        currentCameraPosition = transform.position;
        targetCameraPosition = transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Проверка, не находится ли палец над UI элементом или в области джойстика
            if (IsPointerOverUI(touch) || IsPointerOverJoystick(touch))
                return;

            if (touch.phase == TouchPhase.Moved)
            {
                float touchDeltaX = touch.deltaPosition.x; // Изменение позиции по оси X
                rotationVelocity += touchDeltaX * mouseSensitivity; // Обновление скорости вращения
            }
        }

        // Применение инерции к вращению
        currentAngle += rotationVelocity * Time.deltaTime;
        rotationVelocity *= inertiaDamping;

        // Обновляем угол вращения камеры
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);

        // Плавное движение камеры по высоте (Y)
        AdjustCameraHeight();
    }

    private bool IsPointerOverUI(Touch touch)
    {
        // Создание PointerEventData
        PointerEventData pointerEventData = new PointerEventData(eventSystem)
        {
            position = new Vector2(touch.position.x, touch.position.y)
        };

        // Список для хранения результатов рэйкаста
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        // Возвращаем true, если результаты есть (т.е. палец над UI элементом)
        return results.Count > 0;
    }

    private bool IsPointerOverJoystick(Touch touch)
    {
        // Преобразуем экранные координаты касания в локальные координаты джойстика
        RectTransform joystickRect = joystick.GetComponent<RectTransform>();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickRect, touch.position, null, out localPoint);

        // Проверяем, находится ли точка внутри области джойстика
        return joystickRect.rect.Contains(localPoint);
    }

    // Метод для плавного изменения высоты камеры с учетом "окна"
    private void AdjustCameraHeight()
    {
        if (player != null)
        {
            // Рассчитываем положение окна камеры относительно высоты игрока
            // Это делается через добавление камеры окна к Y позиции игрока
            float targetHeight = Mathf.Clamp(player.position.y + cameraWindowHeight, minHeight, maxHeight);

            // Проверяем, нужно ли камере следовать за игроком по оси Y
            if (Mathf.Abs(player.position.y - transform.position.y) > cameraWindowHeight)
            {
                // Если игрок вышел за пределы окна, камера будет двигаться за ним
                targetHeight = player.position.y;
            }

            // Создаем новую целевую позицию камеры с учетом текущей X и Z координаты
            targetCameraPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

            // Плавно меняем позицию камеры по оси Y
            transform.position = Vector3.Lerp(transform.position, targetCameraPosition, heightSmoothSpeed * Time.deltaTime);
        }
    }
}
