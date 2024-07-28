using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Подключаем пространство имен UI

public class CameraPivot : MonoBehaviour
{
    public float mouseSensitivity = 0.1f; // Скорость изменения угла
    public float rotationSpeed = 5f; // Скорость вращения
    public float inertiaDamping = 0.95f; // Коэффициент затухания инерции
    public FixedJoystick joystick; // Ссылка на фиксированный джойстик из Joystick Pack

    private float currentAngle = 0f; // Текущий угол
    private float rotationVelocity = 0f; // Скорость вращения
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

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

        // Проверка наличия джойстика
        if (joystick == null)
        {
            Debug.LogError("Fixed Joystick not found. Please assign it in the inspector.");
        }
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

        // Применение инерции
        currentAngle += rotationVelocity * Time.deltaTime;

        // Уменьшение скорости вращения для создания эффекта инерции
        rotationVelocity *= inertiaDamping;

        // Обновляем угол камеры
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
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
}