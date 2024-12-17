using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float jumpPower = 10f;
    public float maxSpeed = 5f;
    public float acceleration = 10f;

    public Button jumpButton;
    public Joystick joystick;

    private float gravityForce;
    private Vector3 moveVector;
    private CharacterController characterController;

    private float currentSpeed = 0f;
    public AudioClip sounds;

    public Transform cameraParentTransform; // Родитель камеры, который вращается

    private Renderer playerRenderer; // Компонент для изменения цвета игрока

    // Для льда и батута
    private bool isOnIce = false;  // Флаг, определяющий, на льду ли игрок
    private bool isOnTrampoline = false;  // Флаг, определяющий, на батуте ли игрок

    public float iceFriction = 0.2f;  // Коэффициент скольжения на льду
    public float trampolineForce = 15f;  // Сила отскока от батута

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerRenderer = GetComponent<Renderer>();

        // Проверка на null для кнопки прыжка
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(Jump);
        }
        else
        {
            Debug.LogWarning("Jump button is not assigned.");
        }

        // Применяем цвет скина
        ApplySelectedSkin();
    }

    void Update()
    {
        // Проверка на null для джойстика перед использованием
        if (joystick != null)
        {
            CharacterMove();
        }
        else
        {
            Debug.LogWarning("Joystick is not assigned.");
        }

        ApplyGravity();
    }

    private void ApplySelectedSkin()
    {
        // Загружаем цвет из PlayerPrefs
        float r = PlayerPrefs.GetFloat("SkinColor_R", 1f);
        float g = PlayerPrefs.GetFloat("SkinColor_G", 1f);
        float b = PlayerPrefs.GetFloat("SkinColor_B", 1f);

        Color skinColor = new Color(r, g, b);

        // Применяем цвет к материалу игрока, проверяем на null
        if (playerRenderer != null)
        {
            playerRenderer.material.color = skinColor;
            Debug.Log("Цвет игрока изменён на: " + skinColor);
        }
        else
        {
            Debug.LogWarning("PlayerRenderer is not assigned.");
        }
    }

    private void CharacterMove()
    {
        if (joystick == null) return;

        Vector3 inputDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (inputDirection.magnitude > 1)
            inputDirection.Normalize();

        if (cameraParentTransform != null)
        {
            Quaternion currentCameraRotation = cameraParentTransform.rotation;
            currentCameraRotation.x = 0;
            currentCameraRotation.z = 0;

            inputDirection = currentCameraRotation * inputDirection;
        }

        // Если игрок на льду, уменьшаем его замедление
        if (isOnIce)
        {
            // Уменьшаем скорость на льду, чтобы игрок скользил
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed * iceFriction, acceleration * Time.deltaTime);
        }
        else
        {
            // Обычное движение
            float targetSpeed = inputDirection.magnitude * maxSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }

        moveVector.x = inputDirection.x * currentSpeed;
        moveVector.z = inputDirection.z * currentSpeed;

        if (!characterController.isGrounded)
        {
            moveVector.x *= 0.8f;
            moveVector.z *= 0.8f;
        }

        moveVector.y = gravityForce;
        characterController.Move(moveVector * Time.deltaTime);

        if (inputDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(inputDirection.x, 0, inputDirection.z));
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            gravityForce -= 20f * Time.deltaTime;
        }
        else
        {
            gravityForce = -1f;
        }
    }

    // Прыжок через кнопку
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            if (AudioManager.instance != null && sounds != null)
            {
                AudioManager.instance.PlayPlayerSound(sounds);
            }
            gravityForce = jumpPower;
        }
    }

    // Обработка столкновений с объектами (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ice"))
        {
            isOnIce = true;  // Игрок на льду, активируем скольжение
        }
        else if (other.CompareTag("Trampoline"))
        {
            isOnTrampoline = true;
            BounceOnTrampoline();  // Прыжок от батута
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ice"))
        {
            isOnIce = false;  // Игрок покидает лед, восстанавливаем нормальное движение
        }
        else if (other.CompareTag("Trampoline"))
        {
            isOnTrampoline = false;  // Игрок покидает батут
        }
    }

    // Отскок от батута
    void BounceOnTrampoline()
    {
        if (characterController.isGrounded && isOnTrampoline)
        {
            gravityForce = trampolineForce;  // При столкновении с батутом, применяем силу отскока
        }
    }
}
