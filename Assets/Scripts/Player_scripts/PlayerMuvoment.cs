using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Настройки движения
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

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(Jump);
        }
    }

    void Update()
    {
        CharacterMove();
        ApplyGravity();
    }

    private void CharacterMove()
    {
        // Получаем данные с джойстика
        Vector3 inputDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (inputDirection.magnitude > 1)
            inputDirection.Normalize();

        if (cameraParentTransform != null)
        {
            // Получаем текущую ориентацию родительского объекта камеры
            Quaternion currentCameraRotation = cameraParentTransform.rotation;

            // Снимаем влияние вертикали, чтобы камера только вращалась по горизонтали
            currentCameraRotation.x = 0;
            currentCameraRotation.z = 0;
            currentCameraRotation = Quaternion.Euler(currentCameraRotation.eulerAngles); // Нормализуем ориентацию

            // Преобразуем направление игрока в систему координат камеры
            inputDirection = currentCameraRotation * inputDirection;
        }

        // Плавное изменение скорости
        float targetSpeed = inputDirection.magnitude * maxSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        // Двигаем игрока
        moveVector.x = inputDirection.x * currentSpeed;
        moveVector.z = inputDirection.z * currentSpeed;

        // Уменьшаем скорость в воздухе
        if (!characterController.isGrounded)
        {
            moveVector.x *= 0.8f;
            moveVector.z *= 0.8f;
        }

        moveVector.y = gravityForce;
        characterController.Move(moveVector * Time.deltaTime);

        // Поворот игрока в сторону движения
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

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            AudioManager.instance.PlayPlayerSound(sounds);

            gravityForce = jumpPower;
        }
    }
}
