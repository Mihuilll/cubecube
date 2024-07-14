using UnityEngine;

public class GG_move : MonoBehaviour
{
    //Основные пареметры
    public float jumpPower;
    public float speedMove;

    private float gravityForce; //гравитация персонажа
    private Vector3 moveVector; //напрвление движения 

    //Ссылки на компоненты
    private Rigidbody rb;
    private CharacterController characterController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    //Метод перемещения персонажа
    private void CharacterMove()
    {
        if (characterController.isGrounded)
        {
            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * speedMove;
            moveVector.z = Input.GetAxis("Vertical") * speedMove;

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
              Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }

        moveVector.y = gravityForce;
        characterController.Move(moveVector * Time.deltaTime);
    }
    //Метод гравитации
    private void GamingGravity()
    {
        if (!characterController.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded) gravityForce = jumpPower;
    }
}   