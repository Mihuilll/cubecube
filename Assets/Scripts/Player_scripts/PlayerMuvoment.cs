using UnityEngine;
using UnityEngine.UI;

public class PlayerMuvoment : MonoBehaviour
{
    // player movement
    public float jumpPower;
    public float speedMove;
    public Button jumpButton;   
    public Joystick joystick;   

    private float gravityForce;
    private Vector3 moveVector;
    private Rigidbody rb;
    private CharacterController characterController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(Jump);
        }
    }

    void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    private void CharacterMove()
    {
        if (characterController.isGrounded)
        {
            moveVector = Vector3.zero;
            moveVector.x = joystick.Horizontal * speedMove; // Use joystick input for horizontal movement
            moveVector.z = joystick.Vertical * speedMove;   // Use joystick input for vertical movement

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }
        moveVector.y = gravityForce;
        characterController.Move(moveVector * Time.deltaTime);
    }

    private void GamingGravity()
    {
        if (!characterController.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
    }

    public void Jump()
    {
        if (characterController.isGrounded) gravityForce = jumpPower;
    }
}