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

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerRenderer = GetComponent<Renderer>();

        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(Jump);
        }

        // Применяем цвет скина
        ApplySelectedSkin();
    }

    void Update()
    {
        CharacterMove();
        ApplyGravity();
    }

    private void ApplySelectedSkin()
    {
        // Загружаем цвет из PlayerPrefs
        float r = PlayerPrefs.GetFloat("SkinColor_R", 1f);
        float g = PlayerPrefs.GetFloat("SkinColor_G", 1f);
        float b = PlayerPrefs.GetFloat("SkinColor_B", 1f);

        Color skinColor = new Color(r, g, b);

        // Применяем цвет к материалу игрока
        if (playerRenderer != null)
        {
            playerRenderer.material.color = skinColor;
            Debug.Log("Цвет игрока изменён на: " + skinColor);
        }
    }

    private void CharacterMove()
    {
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

        float targetSpeed = inputDirection.magnitude * maxSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

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

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            AudioManager.instance.PlayPlayerSound(sounds);
            gravityForce = jumpPower;
        }
    }
}
