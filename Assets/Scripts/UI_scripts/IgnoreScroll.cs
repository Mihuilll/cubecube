using UnityEngine;
using UnityEngine.UI;

public class IgnoreScroll : MonoBehaviour
{
    public RectTransform mainHandle; // Основная ручка джойстика
    public RectTransform uiElement; // UI элемент, который будет перемещаться и который является расширенной ручкой
    public RectTransform joystickBackground; // Фон джойстика
    public FixedJoystick joystick; // Джойстик
    private Vector2 originalMainHandlePosition; // Исходная позиция основной ручки
    private Vector2 originalUIElementPosition; // Исходная позиция UI элемента
    private bool isJoystickPressed = false; // Флаг, показывающий, нажата ли ручка джойстика

    void Start()
    {
        if (mainHandle != null)
        {
            originalMainHandlePosition = mainHandle.anchoredPosition; // Сохранение исходной позиции основной ручки
        }

        if (uiElement != null)
        {
            originalUIElementPosition = uiElement.anchoredPosition; // Сохранение исходной позиции UI элемента
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Проверка, находится ли палец в области джойстика
            if (IsPointerOverJoystick(touch))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    isJoystickPressed = true;
                }

                if (isJoystickPressed)
                {
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        Vector2 touchPosition = touch.position;

                        // Конвертируем позицию касания в локальные координаты
                        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, touchPosition, null, out Vector2 localPoint);

                        // Ограничиваем основную ручку внутри фона джойстика
                        Vector2 clampedPoint = Vector2.ClampMagnitude(localPoint, joystickBackground.sizeDelta.x / 2);
                        mainHandle.anchoredPosition = clampedPoint;

                        // Перемещаем UI элемент на основе позиции касания, позволяя ему выходить за пределы
                        uiElement.anchoredPosition = localPoint;
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        isJoystickPressed = false;
                    }
                }
            }
        }
        else
        {
            isJoystickPressed = false;
        }

        // Возвращаем основную ручку и UI элемент в исходное положение, если не происходит перемещение
        if (!isJoystickPressed)
        {
            mainHandle.anchoredPosition = Vector2.Lerp(mainHandle.anchoredPosition, originalMainHandlePosition, Time.deltaTime * 5f);
            uiElement.anchoredPosition = Vector2.Lerp(uiElement.anchoredPosition, originalUIElementPosition, Time.deltaTime * 5f);
        }
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